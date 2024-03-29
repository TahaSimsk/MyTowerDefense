using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (HelperFunctions.CheckImmunity(other.gameObject, Immunity.AllTraps)) return;
            other.GetComponent<EnemyHealth>().ReduceHealth(damage);
            Destroy(gameObject, 0.1f);
        }
    }
}

