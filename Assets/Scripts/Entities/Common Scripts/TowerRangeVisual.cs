using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRangeVisual : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onEscPressed;
    [SerializeField] GameEvent1ParamSO onUIHovered;
    [SerializeField] TargetScanner targetScanner;
    [SerializeField] GameObject upgradeUI;

    bool isHovering;
    MeshRenderer towerRangeMesh;

    private void Awake()
    {
        if (targetScanner != null)
            towerRangeMesh = targetScanner.GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        onEscPressed.onEventRaised += DeactivateUpgradeUI;
        onUIHovered.onEventRaised += CheckHovering;
    }

    private void OnDisable()
    {
        onEscPressed.onEventRaised += DeactivateUpgradeUI;
        onUIHovered.onEventRaised -= CheckHovering;
    }

    private void OnMouseEnter()
    {
        if (isHovering || towerRangeMesh == null) return;
        towerRangeMesh.enabled = true;
    }
    private void OnMouseExit()
    {
        if (isHovering || towerRangeMesh == null) return;
        towerRangeMesh.enabled = false;
    }

    private void OnMouseDown()
    {
        if (upgradeUI != null && !isHovering)
        {
            upgradeUI.SetActive(true);
            if (towerRangeMesh != null)
                towerRangeMesh.enabled = false;
        }
    }



    void DeactivateUpgradeUI()
    {
        isHovering = false;
        if (towerRangeMesh != null)
            towerRangeMesh.enabled = false;
        if (upgradeUI == null) return;
        upgradeUI.SetActive(false);

    }

    void CheckHovering(object isHovering)
    {
        if (isHovering is bool t)
        {
            this.isHovering = t;
        }
    }
}
