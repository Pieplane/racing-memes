using System.Collections;
using System.Collections.Generic;
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

    private bool isPurchased;
    private int speed;
    private int acceleration;
    private int handling;

    private void Start()
    {
        SetDefaultCharacteristicsCar();
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
    private void HandleCarIndexChanged(int newIndex)
    {
        if (newIndex < 0 || newIndex >= carManager.cars.Count)
        {
            Debug.LogWarning("Invalid car index: " + newIndex);
            return;
        }

        isPurchased = carManager.cars[newIndex].isPurchased;
        speed = carManager.cars[newIndex].speed;
        acceleration = carManager.cars[newIndex].acceleration;
        handling = carManager.cars[newIndex].handling;

        SetCharacteristicsCar();

        Debug.Log($"car index {newIndex}");
    }
    private void SetDefaultCharacteristicsCar()
    {
        speedSlider.value = carManager.cars[0].speed;
        accelerationSlider.value = carManager.cars[0].acceleration;
        handlingSlider.value = carManager.cars[0].handling;
    }
    private void SetCharacteristicsCar()
    {
        speedSlider.value = speed;
        accelerationSlider.value = acceleration;
        handlingSlider.value = handling;
    }
}
