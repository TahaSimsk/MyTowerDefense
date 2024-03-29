using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapUpgrade : UpgradeBaseClass
{
    [SerializeField] int upgradeOrder;
    [SerializeField] Button buttonToUnlock;
    FireData fireData;
    private void Awake()
    {
        maxUpgradeCount = 1;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        fireData = (FireData)towerData;
        upgradeCost = fireData.FireTrapUpgradeCosts[upgradeOrder - 1];
    }


    protected override void DoUpgrade()
    {
        switch (upgradeOrder)
        {
            case 1:
                fireData.canThrowTrap = true;
                break;
            case 2:
                fireData.trapCooldown = fireData.UpgradedTrapCooldown1;
                break;
            case 3:
                fireData.trapCooldown=fireData.UpgradedTrapCooldown2;
                break;
            case 4:
                fireData.trapDamage = fireData.UpgradedTrapDamage;
                break;
        }
        if (buttonToUnlock != null)
            buttonToUnlock.interactable = true;
    }

    protected override void HandleUnaffordableUpgrade()
    {
        base.HandleUnaffordableUpgrade();
        //TODO: here goes the logic when an upgrade is not affordable
    }
}
