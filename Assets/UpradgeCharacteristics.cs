using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;

public class UpradgeCharacteristics : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider accelerationSlider;
    [SerializeField] private Slider handlingSlider;
    [SerializeField] private CarSelection carSelection;
    [SerializeField] private CarManager carManager;
    [SerializeField] private TextMeshProUGUI carName;
    [SerializeField] private TextMeshProUGUI priceCost;
    [SerializeField] private TextMeshProUGUI costMoneyForUpgrade;
    [SerializeField] private TextMeshProUGUI costTicketsForUpgrade;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private CarUpgrade carUpgrade;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button buyUpgradeButton;

    private int indexCar;
    private int level = 1;

    private string textName;
    private bool isPurchased;
    private int speed;
    private int acceleration;
    private int handling;

    private void Start()
    {
        indexCar = 0;
        isPurchased = true;
        SetDefaultCharacteristicsCar();
        LoadUpgradeLevels();
        DefineButton();
        PlayerPrefs.DeleteAll();
        Debug.Log($"Car index {indexCar} car level {level}");
    }
    private void LoadUpgradeLevels()
    {
        level = PlayerPrefs.GetInt(indexCar + "UpgradeLevel", 1);
        UpgradeLevel upgradeLevel = carUpgrade.GetUpgradeForLevel(level);

        if (upgradeLevel != null)
        {
            int costMoneyUpgrade = upgradeLevel.costMoney;
            int costTicketsUpgrade = upgradeLevel.costTickets;
            levelTxt.text = $"Уровень {level}";
            costMoneyForUpgrade.text = costMoneyUpgrade.ToString();
            costTicketsForUpgrade.text = costTicketsUpgrade.ToString();

            Debug.Log($"Saved level {level}");
        }
    }
    private void SaveUpgradeLevel()
    {
        PlayerPrefs.SetInt(indexCar + "UpgradeLevel", level);
        PlayerPrefs.Save();
    }
    public void UpgradeLevel()
    {
        if (carManager.cars[indexCar].isPurchased && level < carUpgrade.upgradeLevels.Count)
        {
            bool sucess = carManager.UpgradeCar(indexCar);
            if (sucess)
            {
                level++;
                SaveUpgradeLevel();
                LoadUpgradeLevels();
                DefineButton();
                HandleCarCharacteristics(indexCar);
                SetCharacteristicsCar();
                Debug.Log($"Car index {indexCar} car level {level} sucessed");
            }
            else
            {
                Debug.Log("Whats wrong");
            }
        }
    }

    private void OnEnable()
    {
        if (carSelection != null)
        {
            carSelection.OnCarIndexChanged += HandleCarIndexChanged;
        }

    }
    private void OnDisable()
    {
        if (carSelection != null)
        {
            carSelection.OnCarIndexChanged -= HandleCarIndexChanged;
        }
    }
    public void BuyCar()
    {
        bool purchase = carManager.BuyCar(indexCar);
        if (purchase)
        {
            Debug.Log("Покупка совершена");
            HandleCarIndexChanged(indexCar);
            DefineButton();
        }
        else
        {
            Debug.Log("Не смог купить");
        }
    }
    private void DefineButton()
    {
        if (indexCar >= 0 && isPurchased)
        {
            upgradeButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            priceCost.transform.parent.gameObject.SetActive(false);
            if (level < carUpgrade.upgradeLevels.Count)
            {
                buyUpgradeButton.interactable = true;
                Debug.Log("buyUpgrade true");
            }
            else
            {
                buyUpgradeButton.interactable = false;
                Debug.Log("buyUpgrade false");
            }
        }
        else
        {
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            buyUpgradeButton.interactable = false;
            priceCost.transform.parent.gameObject.SetActive(true);
        }
    }
    private void HandleCarIndexChanged(int newIndex)
    {
        if (newIndex < 0 || newIndex >= carManager.cars.Count)
        {
            Debug.LogWarning("Invalid car index: " + newIndex);
            return;
        }
        indexCar = newIndex;

        isPurchased = carManager.cars[newIndex].isPurchased;
        speed = carManager.cars[newIndex].speed;
        acceleration = carManager.cars[newIndex].acceleration;
        handling = carManager.cars[newIndex].handling;
        textName = carManager.cars[newIndex].carName;
        priceCost.text = carManager.cars[newIndex].price.ToString();

        SetCharacteristicsCar();
        LoadUpgradeLevels();
        DefineButton();
        //Debug.Log($"car index {newIndex}");
    }
    private void HandleCarCharacteristics(int index)
    {
        speed = carManager.cars[index].speed;
        acceleration = carManager.cars[index].acceleration;
        handling = carManager.cars[index].handling;
    }
    private void SetDefaultCharacteristicsCar()
    {
        textName = carManager.cars[0].carName;
        speedSlider.value = carManager.cars[0].speed;
        accelerationSlider.value = carManager.cars[0].acceleration;
        handlingSlider.value = carManager.cars[0].handling;

        carName.text = textName;
    }
    private void SetCharacteristicsCar()
    {
        carName.text = textName;
        speedSlider.value = speed;
        accelerationSlider.value = acceleration;
        handlingSlider.value = handling;
    }
}
