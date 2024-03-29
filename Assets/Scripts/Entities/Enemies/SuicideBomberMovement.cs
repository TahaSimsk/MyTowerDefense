﻿using System.Collections;
using UnityEngine;

public class SuicideBomberMovement : EnemyMovement
{
    [SerializeField] EnemyTargetScanner targetScanner;
    [SerializeField] GameEvent1ParamSO onEnemyDeath;
    protected override IEnumerator MoveAlongPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            while (transform.position != path[i].transform.position + offsetY)
            {
                if (targetScanner != null && targetScanner.targetsInRange.Count > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetScanner.targetsInRange[0].transform.position, currentMoveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, path[i].transform.position + offsetY, currentMoveSpeed * Time.deltaTime);
                }

                yield return null;
            }
        }
        //reaching at the end of the path
        onEnemyReachEndOfPath.RaiseEvent(gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            other.GetComponent<TowerHealth>().ReduceHealth(enemyData.Damage);
            gameObject.SetActive(false);
            onEnemyDeath.RaiseEvent(gameObject);
            //TODO: Play explosion sfx, vfx here
        }
    }
}