using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI tpCooldown;
    [SerializeField] TextMeshProUGUI gameOver;
    int scoreCount = 0;
    //int tpCooldownCount = 0;
    public int turn = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (turn == 0)
        {
            turn = 1;
        }
        else
        {
            turn = 0;
        }
    }

    public void GameOver()
    {
        gameOver.enabled = true;
    }
}
