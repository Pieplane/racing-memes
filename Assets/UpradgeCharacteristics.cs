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
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI ticketsText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button buyButton;

    private int indexCar;

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
        DefineButton();
        //PlayerPrefs.DeleteAll();
    }


    private void OnEnable()
    {
        if(carSelection != null)
        {
            carSelection.OnCarIndexChanged += HandleCarIndexChanged;
        }
        
    }
    private void OnDisable()
    {
        if(carSelection != null)
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
        if(indexCar >= 0 && isPurchased)
        {
            upgradeButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
        }
        else
        {
            upgradeButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
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

        SetCharacteristicsCar();
        DefineButton();

        Debug.Log($"car index {newIndex}");
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
