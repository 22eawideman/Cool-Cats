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
    private int[] enemyPositions;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameManager gameManager;

    int enemyHealth = 1;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemys(int round)
    {
        int enemyCount = 0;
        enemyPositions = calcPositions(round);
        foreach (int position in enemyPositions)
        {
            if (position < 10 && position > - 10)
            {
                float playerPosition = GameObject.Find("Player").transform.position.x;
                if (position + .5f == playerPosition || position - .5f == playerPosition)
                {

                }
                else if (position > 0)
                {
                    enemyCount++;
                    Vector3 rightSpawnPos = rightSpawn.transform.position;
                    Vector3 enemySpawnPos = new Vector3(rightSpawnPos.x, -3.37f, -1);
                    GameObject enemyInstance = Instantiate(enemy, enemySpawnPos, Quaternion.identity);
                    int offset = position;
                    float enemyY = enemyInstance.transform.position.y;
                    Vector3 newPosition = new Vector3(offset - .5f, enemySpawnPos.y, -1);
                    enemyInstance.GetComponent<EnemyScript>().moveEnemy(newPosition);
                    enemyInstance.GetComponent<EnemyScript>().setSide(1);
                    enemyInstance.GetComponent<EnemyScript>().setHealth(enemyHealth);
                    enemyInstance.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (position < 0)
                {
                    enemyCount++;
                    Vector3 leftSpawnPos = leftSpawn.transform.position;
                    Vector3 enemySpawnPos = new Vector3(leftSpawnPos.x, -3.37f, -1);
                    GameObject enemyInstance = Instantiate(enemy, enemySpawnPos, Quaternion.identity);
                    int offset = position;
                    float enemyY = enemyInstance.transform.position.y;
                    Vector3 newPosition = new Vector3(offset + .5f, enemySpawnPos.y, -1); 
                    enemyInstance.GetComponent<EnemyScript>().moveEnemy(newPosition);
                    enemyInstance.GetComponent<EnemyScript>().setSide(0);
                    enemyInstance.GetComponent<EnemyScript>().setHealth(enemyHealth);
                }
                
            }
        }
        gameManager.setEnemyCount(enemyCount);
        
    }

    private int[] calcPositions(int round)
    {
        if (round % 2 == 0)
        {
            enemyHealth++;
        }
        int healthOffset = round % 2;
        int numEnemies = 2 + (healthOffset * 2);
        int[] positions = new int[numEnemies];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = UnityEngine.Random.Range(-9, 9);
        }
        return positions;
    }
}
