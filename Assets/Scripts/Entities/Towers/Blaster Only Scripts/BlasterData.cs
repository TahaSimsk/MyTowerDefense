using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Towers/Blaster")]
public class BlasterData : TowerData
{

    [Header("---------------------------BURST ATTRIBUTES-----------------------")]
    public float TimeBetweenBursts;
    public int BurstCount;
    public float AmmoEfficiency;

    [Header("---------------------------BURST UPGRADES-----------------------")]

    public List<int> BurstUpgradedValues;
    public bool canDoubleBarrel;
    public List<float> BurstUpgradeMoneyCosts;
    public List<float> BurstUpgradeWoodCosts;
    public List<float> BurstUpgradeRockCosts;

    [Header("---------------------------AMMO UPGRADES-----------------------")]

    public List<float> AmmoEfficiencyUpgradedValues;
    public int UpgradedAmmoCapacity;
    public List<float> AmmoUpgradeMoneyCosts;
    public List<float> AmmoUpgradeWoodCosts;
    public List<float> AmmoUpgradeRockCosts;

}
