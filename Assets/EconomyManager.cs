using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyTxt;
    [SerializeField] private TextMeshProUGUI ticketsTxt;
    [SerializeField] private UIEffects uiEffects;

    public int money;
    public int tickets;

    private void Start()
    {
        LoadEconomyData();
        moneyTxt.text = money.ToString();
        ticketsTxt.text = tickets.ToString();
    }

    public bool CanAfford(int carPrice, int ticketCost)
    {
        return money >= carPrice && tickets >= ticketCost;
    }
    public void Spend(int amount, int ticketAmount)
    {
        money -= amount;
        tickets -= ticketAmount;
        moneyTxt.text = money.ToString();
        ticketsTxt.text = tickets.ToString();
        SaveEconomyData();
        Refusal();
    }
    private void LoadEconomyData()
    {
        money = PlayerPrefs.GetInt("Money", 1000);
        tickets = PlayerPrefs.GetInt("Tickets", 10);
    }
    private void SaveEconomyData()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Tickets", tickets);
        PlayerPrefs.Save();
    }
    public void AddMoney(int amount)
    {
        money += amount;
        SaveEconomyData ();
    }
    public void AddTickets(int amount)
    {
        tickets += amount;
        SaveEconomyData ();
    }
    private void Refusal()
    {
        uiEffects.Attention(shakeDuration: 0.5f, shakeStrength: 10f, vibrato: 10, colorFlashDuration: 0.2f, target: moneyTxt.transform.parent);
    }
}
