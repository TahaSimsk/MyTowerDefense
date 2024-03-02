using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplosionRadiusUpgrade : UpgradeBaseClass
{
    public int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    CannonData cannonData;
    CannonData defCannonData;

    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        cannonData = (CannonData)towerData;
        defCannonData = (CannonData)towerInfo.DefTowerData;

        upgradeCost = cannonData.ExplosionRadiusUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
            cannonData.ExplosionRadius = (defCannonData.ExplosionRadius * cannonData.ExplosionRadiusUpgradeValues[upgradeOrder - 1] * 0.01f) + defCannonData.ExplosionRadius;
        

        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;
    }

    protected override void HandleUnaffordableUpgrade()
    {
        base.HandleUnaffordableUpgrade();
        //TODO: here goes the logic when an upgrade is not affordable
    }
}
