using System;
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

    [Header("Costs for Skills")]
    public float bombCost;
    public float slowCost;

    [Header("Costs for Demolish")]
    public float oneTreeDemolishCost;
    public float doubleTreeDemolishCost;
    public float quadTreeDemolishCost;


    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] float startingBalance;
    float money;

    private void OnEnable()
    {
        EventManager.onEnemyDeath += AddMoney;
        EventManager.onTowerPlaced += DecreaseMoney;
    }
    private void OnDisable()
    {
        EventManager.onEnemyDeath -= AddMoney;
        EventManager.onTowerPlaced -= DecreaseMoney;
    }

    private void Start()
    {
        money = startingBalance;
        UpdateMoneyDisplay();

        EventManager.OnMoneyIncreased(money);
        EventManager.OnMoneyDecreased(money);
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
        //money += amount;
        //EventManager.OnMoneyChanged();
    }

    //this is used for enemy death, gets the data from the died enemy and gets the cost, ignore gameobject
    public void AddMoney(GameObject enemy)
    {
        //money += enemy.GetComponent<EnemyHealth>().enemyData.objectCost_MoneyDrop;
        //EventManager.OnMoneyChanged();
        //EventManager.OnMoneyIncreased(money);
        //UpdateMoneyDisplay();
    }

    public void DecreaseMoney(float amount)
    {
        money -= amount;
        EventManager.OnMoneyChanged();
        EventManager.OnMoneyDecreased(money);
        UpdateMoneyDisplay();
    }

    //this is used for tower placement, gets the data from last placed tower and gets the cost
    public void DecreaseMoney(GameObject gameObject)
    {
        money -= gameObject.GetComponent<Tower>().towerData.objectCost_MoneyDrop;
        EventManager.OnMoneyChanged();
        EventManager.OnMoneyDecreased(this.money);
        Debug.Log("Money Decreased");
        UpdateMoneyDisplay();
    }

    public void UpdateMoneyDisplay()
    {
        //moneyText.text = "$" + Mathf.FloorToInt(money);
    }


}
