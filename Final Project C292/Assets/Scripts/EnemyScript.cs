using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.UI;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int points;
    GameManager gameManager;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bullet;

    bool isCover = false;
    int side;
    Vector3 newPosition;
    bool isMoving = false;
    int health = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();
        checkShoot();
    }

    private void checkMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed);
        if (newPosition == transform.position)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    public void checkShoot()
    {
        if (!isMoving && gameManager.turn == 1 && !isCover)
        {
            gameManager.changeTurn();
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(0,0,90f));
        }
        if (isCover && gameManager.turn == 1)
        {
            gameManager.changeTurn();
        }
    }

    public void hit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            destroyEnemy();
        }
    }

    public void destroyEnemy()
    {
        
        Destroy(gameObject);
        gameManager.increaseScore(points);
        gameManager.enemiesKilled++;
    }

    public void moveEnemy(Vector3 position)
    {
        newPosition = position;
    }

    public void setSide(int side)
    {
        this.side = side;
    }

    public void setIsCover()
    {
        if (isCover)
        {
            isCover = false;
        }
        else
        {
            isCover = true; 
        }
    }

    public bool enemyDestroyEnemy()
    {
        if (isCover)
        {
            PlayerScript player = GameObject.Find("Player").GetComponent<PlayerScript>();
            player.killCover();
            Destroy(gameObject);
            gameManager.increaseScore(points);
            gameManager.enemiesKilled++;
            return true;

        }
        return false;
    }
  
    public void setHealth(int health)
    {
        this.health = health;
    }
}
