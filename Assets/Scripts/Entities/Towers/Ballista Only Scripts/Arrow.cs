using System;
using UnityEngine;

public class Arrow : Projectile
{
    int pierceCount;

    float timer;
    BallistaData ballistaData;
    bool canPoison;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (towerData == null) return;
        ballistaData = (BallistaData)towerData;
        timer = 0;
        pierceCount = 0;
        //if (initiated)
            canPoison = ballistaData.canPoison;
    }

    protected override void Update()
    {
        MoveToTarget();
    }

    protected override void MoveToTarget()
    {
        if (target != null)
            transform.LookAt(target.position + Vector3.up * 4);
        target = null;
        timer += Time.deltaTime;
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
        if (timer >= ballistaData.projectileLife)
        {
            gameObject.SetActive(false);
        }

    }


    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (other.GetComponent<EnemyHealth>().ReduceHealth(damage))
        {
            xpManager.GainXp(); 
        }


        PoisonBehaviour(other);



        PierceBehaviour(other);

    }

    private void PoisonBehaviour(Collider other)
    {
        if (canPoison && !HelperFunctions.CheckImmunity(other.gameObject, Immunity.Poison))
        {
            if (UnityEngine.Random.Range(0, 100) <= ballistaData.poolDropChance)
            {
                RaycastHit hit;
                if (Physics.SphereCast(other.transform.position + Vector3.up * 4f, 1f, Vector3.down, out hit, 6f, ballistaData.poolLayer))
                {
                    PoisonField pool = hit.transform.GetComponent<PoisonField>();

                    if (hit.transform.CompareTag("Path2") && pool == null)
                    {
                        GameObject poolObject = Instantiate(ballistaData.poisonPool, hit.transform.position + Vector3.up * 2, Quaternion.identity);
                        poolObject.GetComponent<PoisonField>().SetDurationsAndDamage(ballistaData.poolDuration, ballistaData.poisonDurationOnEnemies, ballistaData.poolDamage);
                    }
                    else if (pool != null)
                    {
                        pool.SetDurationsAndDamage(ballistaData.poolDuration, ballistaData.poisonDurationOnEnemies, ballistaData.poolDamage);
                    }

                }

            }
            canPoison = !ballistaData.dropPoolOnFirstEnemy;
        }
    }

    private void PierceBehaviour(Collider other)
    {
        if (ballistaData.canPierce && !HelperFunctions.CheckImmunity(other.gameObject, Immunity.Pierce))
        {
            pierceCount++;

            if (pierceCount > ballistaData.pierceLimit)
            {

                gameObject.SetActive(false);
                return;
            }
            damage = ballistaData.ProjectileDamage - (ballistaData.ProjectileDamage * ballistaData.pierceDamage[pierceCount - 1] * 0.01f);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


}
