using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataProjectile", menuName = "DataProjectile")]
public class DataProjectile : ScriptableObject
{
    public string nameOfProjectile;


    [Header("Projectile Prefabs")]
    public GameObject projectile;

    [Header("Projectile Attributes")]
    public float projectileSpeed;
    public float projectileDamage;
    public float projectileLife;
}