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

    int side;
    Vector3 newPosition;
    bool isMoving = false;
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
        if (!isMoving && gameManager.turn == 1)
        {
            gameManager.changeTurn();
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(0,0,90f));
            bullet.GetComponent<BulletScript>().side = side;
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
        gameManager.increaseScore(points);
    }

    public void moveEnemy(Vector3 position)
    {
        newPosition = position;
    }

    public void setSide(int side)
    {
        this.side = side;
    }
}
