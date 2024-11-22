using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        GameObject player = GameObject.Find("Player");
        Vector2 newPosition;
        if (player != null)
        {
            newPosition = new Vector2(player.transform.position.x, transform.position.y);
        }
        else
        {
            newPosition = transform.position;
        }
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
        
        gameObject.transform.position = Vector2.MoveTowards(curPosition, newPosition, moveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool canHit = true;
            GameObject playerInstance = collision.gameObject;
            if (checkCovered(playerInstance) == 2)
            {
                if (transform.position.x < playerInstance.transform.position.x)
                {
                    canHit = false;
                }
            }
            else if (checkCovered(playerInstance) == 1)
            {
                if (transform.position.x > playerInstance.transform.position.x)
                {
                    canHit = false;
                }
            }
            else if (checkCovered(playerInstance) == 3)
            {
                canHit = false;
            }
            if (canHit)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                gameManager.GameOver();
            }
        }
        if (!collision.gameObject.CompareTag("EnemyHead") && !collision.gameObject.CompareTag("EnemyBody") && !collision.gameObject.CompareTag("Cover"))
        {
            Destroy(gameObject);
        }
        else
        {
            bool kill = collision.gameObject.GetComponentInParent<EnemyScript>().enemyDestroyEnemy();
            if (kill)
            {
                Destroy(gameObject);
            } 
        }
    }

    private int checkCovered(GameObject player)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        if (playerScript.isCoveredLeft && playerScript.isCoveredRight)
        {
            return 3;
        }
        if (playerScript.isCoveredLeft)
        {
            return 2;
        }
        else if (playerScript.isCoveredRight)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
