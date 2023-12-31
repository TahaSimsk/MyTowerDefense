using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] float timeBetweenEnemySpawns;
    [SerializeField] float timeBetweenEnemyWaves;

    [SerializeField] List<Waves> waves = new List<Waves>();

    [SerializeField] bool setEnemyHealthManually;
    [SerializeField] bool enemyHealthDifficulty;
    [SerializeField][Range(1, 3)] float enemyHealthDifficultyMultiplier;

    ObjectPool objectPool;
    UIManager uiManager;
    List<GameObject> enemiesInWave = new List<GameObject>();

    int currentWave;
    public int numOfTotalEnemies;

    float difficulty = 1f;
    float enemyHealth;


    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        uiManager = FindObjectOfType<UIManager>();
        StartCoroutine(SpawnEnemies());
    }


    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            //start the wave
            foreach (var wave in waves)
            {
                currentWave++;

                GetTotalEnemiesInCurrentWave(wave.enemyCount);
                uiManager.UpdateRemainingEnemiesText(true);
                uiManager.UpdateWaveText(currentWave);


                //loop through enemy types in the wave
                for (int i = 0; i < wave.enemyCount.Count; i++)
                {
                    int enemyCount = wave.enemyCount[i];
                    int enemyId = wave.enemies[i].GetComponent<EnemyHealth>().enemyId;

                    if (setEnemyHealthManually && (wave.enemyHealth != null || wave.enemyHealth[i] != Mathf.Epsilon))
                    {
                        enemyHealth = wave.enemyHealth[i];
                    }

                    //spawn enemies by their count which is set in the "Wave SO"
                    for (int x = 0; x < enemyCount; x++)
                    {
                        GameObject pooledEnemy = objectPool.GetEnemy(enemyId);
                        enemiesInWave.Add(pooledEnemy);

                        SetHealthDifficulty(pooledEnemy);

                        pooledEnemy.SetActive(true);

                        
                        yield return new WaitForSeconds(timeBetweenEnemySpawns);
                    }

                }
                //end of the wave

                //wait for enemies to die before starting countdown to go to the next wave
                foreach (var enemy in enemiesInWave)
                {
                    yield return new WaitUntil(() => enemy.activeInHierarchy == false);
                }
                enemiesInWave.Clear();


                //next wave countdown
                uiManager.GetNextWaveTimer(true, timeBetweenEnemyWaves);
                yield return new WaitForSeconds(timeBetweenEnemyWaves);

                IncreaseDifficultyForNextWave();
            }
            //end of total waves
            break;
        }
    }

    private void SetHealthDifficulty(GameObject pooledEnemy)
    {
        if (setEnemyHealthManually && enemyHealth != Mathf.Epsilon)
        {
            pooledEnemy.GetComponent<EnemyHealth>().maxHealth = enemyHealth;
        }
        else if (enemyHealthDifficulty)
        {
            pooledEnemy.GetComponent<EnemyHealth>().SetMaxHP(difficulty);
        }
    }

    private void IncreaseDifficultyForNextWave()
    {
        //if automatic enemy health difficulty is toggled in hierarchy, increase the health of the enemies in the next wave by given amount
        if (enemyHealthDifficulty)
        {
            difficulty *= enemyHealthDifficultyMultiplier;
        }
    }



    void GetTotalEnemiesInCurrentWave(List<int> enemyCount)
    {
        foreach (var item in enemyCount)
        {
            numOfTotalEnemies += item;
        }
    }



}
