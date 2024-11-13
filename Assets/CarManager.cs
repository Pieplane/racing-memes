using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public List<Car> cars;
    public EconomyManager economyManager;

    private void Start()
    {
        LoadCarData();
    }
    public bool BuyCar(int carIndex)
    {
        Car car = cars[carIndex];
        if(!car.isPurchased && economyManager.CanAfford(car.price, car.ticketCost))
        {
            economyManager.Spend(car.price, car.ticketCost);
            car.isPurchased = true;
            SaveCarData();
            return true;
            
        }
        return false;
    }
    public bool UpgradeCar(int carIndex, string attribute) 
    {
        Car car = cars[carIndex];
        if(car.isPurchased && economyManager.money >= car.upgradeCostMoney && economyManager.tickets >= car.upgradeCostTickets)
        {
            economyManager.Spend(car.upgradeCostMoney, car.upgradeCostTickets);

            switch(attribute)
            {
                case "speed":
                    car.UpgradeSpeed();
                    break;
                case "acceleration":
                    car.UpgradeAcceleration();
                    break;
                case "handling":
                    car.UpgradeHandling();
                    break;
                default:
                    return false;
            }
            SaveCarData();
            return true;
        }
        return false;
    }
    private void LoadCarData()
    {
        for (int i = 0; i < cars.Count; i++)
        {
            string carKey = "Car_" + i;
            if (i == 0)
            {
                cars[i].isPurchased = true;
                PlayerPrefs.SetInt(carKey + "Purchased", 1);
            }
            else
            {
                cars[i].isPurchased = PlayerPrefs.GetInt(carKey + "Purchased", 0) == 1;
            }
            cars[i].speed = PlayerPrefs.GetInt(carKey + "Speed", cars[i].speed);
            cars[i].acceleration = PlayerPrefs.GetInt(carKey + "Acceleration", cars[i].acceleration);
            cars[i].handling = PlayerPrefs.GetInt(carKey + "Handling", cars[i].handling);
        }
    }
    private void SaveCarData()
    {
        for(int i = 0; i < cars.Count; i++)
        {
            string carKey = "Car_" + i;
            PlayerPrefs.SetInt(carKey + "Purchased", cars[i].isPurchased ? 1 : 0);
            PlayerPrefs.SetInt(carKey + "Speed", cars[i].speed);
            PlayerPrefs.SetInt(carKey + "Acceleration", cars[i].acceleration);
            PlayerPrefs.SetInt(carKey + "Handling", cars[i].handling);
        }
        PlayerPrefs.Save();
    }
}

