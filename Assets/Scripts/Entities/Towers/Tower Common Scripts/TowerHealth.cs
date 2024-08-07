using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour, IPlayerDamageable
{
    [Header("Events")]
    [SerializeField] GameEvent1ParamSO onTowerDeath;
    [SerializeField] ObjectInfo towerInfo;
    [SerializeField] Slider healthBar;
    [SerializeField] CameraShake cameraShake;


    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    float baseMaxHealth;


    private void Awake()
    {
        baseMaxHealth = towerInfo.DefTowerData.BaseMaxHealth;
        MaxHealth = baseMaxHealth;
    }


    private void OnEnable()
    {
        ResetHP();
        UpdateHPBar();
    }


    private void CheckIfDiedAndHandleDeath()
    {
        if (CurrentHealth <= 0)
        {
            onTowerDeath.RaiseEvent(gameObject);

            gameObject.SetActive(false);
        }
    }


    public void AddHealth(float _amount)
    {
        CurrentHealth += _amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UpdateHPBar();
    }


    public void SetMaxHP(float amount)
    {
        float newMaxHPValue = HelperFunctions.CalculatePercentage(baseMaxHealth, amount);
        float upgradeAmount = newMaxHPValue - MaxHealth;
        MaxHealth = newMaxHPValue;
        AddHealth(upgradeAmount);
        UpdateHPBar();
    }


    public void ResetHP()
    {
        CurrentHealth = MaxHealth;
        UpdateHPBar();
    }


    void UpdateHPBar()
    {
        healthBar.value = CurrentHealth / MaxHealth;
    }


    public void GetDamage(float damage)
    {
        CurrentHealth -= damage;
        if (cameraShake != null)
        {
            StartCoroutine(cameraShake.Shake(.2f));
        }
        UpdateHPBar();
        CheckIfDiedAndHandleDeath();
    }
}
