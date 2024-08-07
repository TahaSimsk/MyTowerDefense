﻿using UnityEngine;

public class EnemyRangedShooting : MonoBehaviour
{
    [SerializeField] ObjectInfo enemyInfo;
    [SerializeField] EnemyTargetScanner targetScanner;
    [SerializeField] Transform projectilePos;
    [SerializeField] Transform partToRotate;

    float timer;

    EnemyData enemyData;


    void Start()
    {
        enemyData = enemyInfo.DefObjectGameData as EnemyData;
    }


    void Update()
    {

        Shoot();
    }


    void Shoot()
    {
        timer += Time.deltaTime;

        if (targetScanner.targetsInRange.Count == 0) return;

        HelperFunctions.LookAtTarget(targetScanner.targetsInRange[0].transform.position, partToRotate, enemyData.AimSpeed);

        if (timer < enemyData.ShootingDelay) return;

        GetProjectileFromPoolAndActivate(projectilePos);
        timer = 0;

    }
    
    /*
         * when projectile pooled and activated, shooting starts. The script in the projectile handles movement and collision. In this script all we need to do is activate it and pass the target.
         */
    void GetProjectileFromPoolAndActivate(Transform projectileSpawnPoint)
    {
        GameObject pooledProjectile = ObjectPool.Instance.GetObject(enemyData.ProjectilePrefab.GetHashCode(), enemyData.ProjectilePrefab);

        if (pooledProjectile == null || targetScanner.targetsInRange.Count == 0) return;
        EnemyProjectile projectile = pooledProjectile.GetComponent<EnemyProjectile>();
        projectile.SetProjectile(enemyData);

        projectile.target = targetScanner.targetsInRange[0].transform;

        pooledProjectile.transform.position = projectileSpawnPoint.position;
        pooledProjectile.SetActive(true);
        pooledProjectile.transform.LookAt(projectile.target);
    }
}