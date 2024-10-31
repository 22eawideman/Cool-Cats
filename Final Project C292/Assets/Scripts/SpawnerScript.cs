using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject leftSpawn;
    [SerializeField] GameObject rightSpawn;
    [SerializeField] int[] enemyPositions;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
       Array.Sort(enemyPositions);
        spawnEnemys();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnEnemys()
    {
        foreach (int position in enemyPositions)
        {
            if (position < 19 && position > - 19)
            {
                if (position > 0)
                {
                    Vector3 rightSpawnPos = rightSpawn.transform.position;
                    Vector3 enemySpawnPos = new Vector3(rightSpawnPos.x, -2.955f, -1);
                    GameObject enemyInstance = Instantiate(enemy, enemySpawnPos, Quaternion.identity);
                    int offset = position / 2;
                    float enemyY = enemyInstance.transform.position.y;
                    Vector3 newPosition = new Vector3(offset - .5f, enemySpawnPos.y, -1);
                    enemyInstance.GetComponent<EnemyScript>().moveEnemy(newPosition);
                    enemyInstance.GetComponent<EnemyScript>().setSide(1);

                }
                else if (position < 0)
                {
                    Vector3 leftSpawnPos = leftSpawn.transform.position;
                    Vector3 enemySpawnPos = new Vector3(leftSpawnPos.x, -2.955f, -1);
                    GameObject enemyInstance = Instantiate(enemy, enemySpawnPos, Quaternion.identity);
                    int offset = position / 2;
                    float enemyY = enemyInstance.transform.position.y;
                    Vector3 newPosition = new Vector3(offset + .5f, enemySpawnPos.y, -1); 
                    enemyInstance.GetComponent<EnemyScript>().moveEnemy(newPosition);
                    enemyInstance.GetComponent <EnemyScript>().setSide(0);
                }
                
            }
        }
        
    }
}
