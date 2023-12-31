using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    [Header("Costs to Place Towers")]
    public float ballistaCost;
    public float blasterCost;
    public float cannonCost;


    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] float startingBalance;
    float money;

    private void Start()
    {
        money = startingBalance;
        UpdateMoneyDisplay();
    }

    public bool IsPlaceable(float towerCost)
    {
        if (money >= towerCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }

    public void DecreaseMoney(float amount)
    {
        money -= amount;
    }

    public void UpdateMoneyDisplay()
    {
        moneyText.text = "$" + Mathf.FloorToInt(money);
    }


}
