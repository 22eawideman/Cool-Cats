using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI tpCooldown;
    [SerializeField] TextMeshProUGUI gameOver;
    [SerializeField] GameObject spawner;

    private SpawnerScript spawnerScript;
    int scoreCount = 0;
    //int tpCooldownCount = 0;
    public int turn = 0;
    public int round = 0;
    public int enemiesKilled = 0;
    int numEnemys = 0;
    int moveCount = 0;
    int playerMoveCount = 1;
    int playerMoves = 0;
    bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnerScript = spawner.GetComponent<SpawnerScript>();
        spawnerScript.spawnEnemys(round);
    }

    // Update is called once per frame
    void Update()
    {
        checkRestart();
    }

    public void changeTPCooldown(int time)
    {
        tpCooldown.text = "Teleport: " + time + "s";
    }

    public void increaseScore(int points)
    {
        scoreCount += points;
        score.text = "Score: " + scoreCount;
    }

    public void changeTurn()
    {
        bool noEnemies = checkNextRound();
        if (turn == 0 && !noEnemies)
        {
            if (playerMoveCount == playerMoves)
            {
                turn = 1;
                playerMoves = 0;
            }
            else
            {
                playerMoves++;
            }

        }
        else
        {
            if (noEnemies)
            {

            }
            else if (moveCount == numEnemys)
            {
                turn = 0;
                moveCount = 0;
            }
            else
            {
                moveCount++;
            }
        }
    }

    public void GameOver()
    {
        gameOver.enabled = true;
        isGameOver = true;
    }

    public void setEnemyCount(int count)
    {
        numEnemys = count;
    }

    private void checkRestart()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("StartingScene");
            }
        }
    }

    private bool checkNextRound()
    {
        if (enemiesKilled == numEnemys)
        {
            round++;
            enemiesKilled = 0;
            spawnerScript.spawnEnemys(round);
            turn = 0;
            moveCount = 0;
            playerMoves = 0;
            return true;
        }
        return false;
    }

}
