using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameUpgrade : UpgradeBaseClass
{
    [SerializeField] int upgradeOrder;
    [SerializeField] GameObject flame;
    [SerializeField] Button buttonToUnlock;

    FireData fireData;

    protected override void Awake()
    {
        base.Awake();
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        fireData = (FireData)towerData;
        upgradeMoneyCost = fireData.flameUpgradeMoneyCosts[upgradeOrder - 1];
        upgradeWoodCost = fireData.flameUpgradeWoodCosts[upgradeOrder - 1];
        upgradeRockCost = fireData.flameUpgradeRockCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        flame.SetActive(true);
        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;
    }

    protected override void HandleUnaffordableUpgrade()
    {
        base.HandleUnaffordableUpgrade();
        //TODO: here goes the logic when an upgrade is not affordable
    }
}
