using System.Collections.Generic;
using UnityEngine;

public class TargetScanner : MonoBehaviour
{
    [SerializeField] string targetTag;
    [SerializeField] protected ObjectInfo objectInfo;
    [SerializeField] GameEvent1ParamSO onTargetDeath;

    [HideInInspector] public List<GameObject> targetsInRange = new List<GameObject>();


    protected virtual void OnEnable()
    {
        onTargetDeath.onEventRaised += RemoveTarget;
    }


    protected virtual void OnDisable()
    {
        onTargetDeath.onEventRaised -= RemoveTarget;

    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetsInRange.Add(other.gameObject);
        }
    }


    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {

            targetsInRange.Remove(other.gameObject);

        }
    }


    protected virtual void RemoveTarget(object target)
    {
        if (target is GameObject && targetsInRange.Contains((GameObject)target))
        {
            targetsInRange.Remove((GameObject)target);
        }
    }
}
