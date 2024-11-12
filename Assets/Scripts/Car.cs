using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Car : MonoBehaviour
{
    public string carName;
    public int price;
    public int ticketCost;
    public bool isPurchased = false;

    public int speed = 100;
    public int handling = 50;
    public int acceleration = 60;

    public int upgradeCost = 500;

    public void UpgradeSpeed() => speed += 10;
    public void UpgradeHandling() => handling += 5;
    public void UpgradeAcceleration() => acceleration += 7;
}
