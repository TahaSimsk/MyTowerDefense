﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerData : GameData
{
    [Header("---------------------------TOWER PREFABS-----------------------")]
    public GameObject TowerPrefab;
    public GameObject TowerHoverPrefab;
    public GameObject TowerNPHoverPrefab;

    [Header("---------------------------EVENTS-----------------------")]
    [SerializeField] GameEvent1ParamSO onAmmoRefillCostReductionUpgrade;
    [SerializeField] GameEvent1ParamSO onAmmoRefillAmountUpgrade;

    [Header("---------------------------TOWER ATTRIBUTES-----------------------")]
    public TargetPriority TargetPriority;
    public float ShootingDelay;
    public float TowerRange;
    public float BaseMaxHealth;
    public float TowerRotationSpeed;
    public float TowerPlacementCost;
    public float TowerHPRefillCost;
    public float TowerAmmoRefillCost;
    public int TowerAmmoRefillAmount;
    public int TowerAmmoCapacity;
    public float ProjectileSpeed;
    public float ProjectileDamage;

    [Header("---------------------------TOWER UPGRADES-----------------------")]
    [Header("Shooting Delay")]
    public List<float> ShootingDelayUpgradeValues;
    public List<float> ShootingDelayUpgradeCosts;

    [Header("Range")]
    public List<float> RangeUpgradeValues;
    public List<float> RangeUpgradeCosts;

    [Header("HP")]
    public List<float> MaxHealthUpgradeValues;
    public List<float> MaxHealthUpgradeCosts;

    [Header("Damage")]
    public List<float> ProjectileDamageUpgradeValues;
    public List<float> ProjectileDamageUpgradeCosts;



    float originalAmmoRefillCost;
    int originalAmmoRefillAmount;


    private void OnEnable()
    {
        StoreOriginalValues();

        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    void SubscribeEvents()
    {
        SceneManager.activeSceneChanged += RestoreOriginalValues;
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged += RestoreOriginalValues;
#endif

        SubscribeEvent(onAmmoRefillCostReductionUpgrade, HandleAmmoRefillCostReductionUpgrade);
        SubscribeEvent(onAmmoRefillAmountUpgrade, HandleAmmoRefillAmountUpgrade);
    }

    void UnSubscribeEvents()
    {
        SceneManager.activeSceneChanged += RestoreOriginalValues;
#if UNITY_EDITOR
        EditorApplication.playModeStateChanged -= RestoreOriginalValues;
#endif

        UnSubscribeEvent(onAmmoRefillCostReductionUpgrade, HandleAmmoRefillCostReductionUpgrade);
        UnSubscribeEvent(onAmmoRefillAmountUpgrade, HandleAmmoRefillAmountUpgrade);
    }


    void StoreOriginalValues()
    {
        originalAmmoRefillCost = TowerAmmoRefillCost;
        originalAmmoRefillAmount = TowerAmmoRefillAmount;
    }

    void OriginalValues()
    {
        TowerAmmoRefillCost = originalAmmoRefillCost;
        TowerAmmoRefillAmount = originalAmmoRefillAmount;
    }


    void RestoreOriginalValues(Scene arg, Scene arg2)
    {
        OriginalValues();
    }

#if UNITY_EDITOR
    void RestoreOriginalValues(PlayModeStateChange mode)
    {
        if (mode == PlayModeStateChange.ExitingPlayMode)
        {
            OriginalValues();
        }
    }
#endif
  
   
  

    void SubscribeEvent(GameEvent1ParamSO _event, UnityEngine.Events.UnityAction<object> _function)
    {
        if (_event != null)
        {
            _event.onEventRaised += _function;
        }
    }

    void UnSubscribeEvent(GameEvent1ParamSO _event, UnityEngine.Events.UnityAction<object> _function)
    {
        if (_event != null)
        {
            _event.onEventRaised -= _function;
        }
    }

    void HandleAmmoRefillCostReductionUpgrade(object _amount)
    {
        TowerAmmoRefillCost = HelperFunctions.CalculatePercentage(TowerAmmoRefillCost, (float)_amount, false);
    }

    void HandleAmmoRefillAmountUpgrade(object _amount)
    {
        TowerAmmoRefillAmount =(int)HelperFunctions.CalculatePercentage((float)TowerAmmoRefillAmount, (float)_amount, true);
    }

}


