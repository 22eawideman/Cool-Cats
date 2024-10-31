using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float tpCooldown;
    [SerializeField] GameManager gameManager;
    Vector3 newPosition;
    bool isMoving = false;
    EnemyScript enemy;
    float timeOfNextTP = 0;
    //bool isCoveredLeft = false;
    //bool isCoveredRight = false;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();
        checkTP();
    }

    void checkMove()
    {
        if (!isMoving && !Input.GetKey(KeyCode.C) && gameManager.turn == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 raycastPos = new Vector3(mousePos.x, mousePos.y, -10);
                RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (hit)
                {

                    if (hit.collider.CompareTag("EnemyHead") || hit.collider.CompareTag("EnemyBody"))
                    {
                        Debug.Log("hit enemy");
                        GameObject enemyCollider = hit.collider.gameObject;
                        enemy = enemyCollider.transform.parent.gameObject.GetComponent<EnemyScript>();
                        enemy.destroyEnemy();
                    }
                    else if (hit.collider.CompareTag("MoveZone"))
                    {
                        Debug.Log("hit");
                        float xPos = hit.collider.transform.position.x;
                        newPosition = new Vector3 (xPos, transform.position.y, -1);
                        isMoving = true;
                    }
                    gameManager.changeTurn();
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed);
        if (transform.position == newPosition)
        {
            isMoving = false;
        }
    }

    void checkTP()
    {
        if (Input.GetKey(KeyCode.C) && Time.realtimeSinceStartup >= timeOfNextTP && gameManager.turn == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 raycastPos = new Vector3(mousePos.x, mousePos.y, -10);
                RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (hit)
                {
                    if (hit.collider.CompareTag("EnemyHead") || hit.collider.CompareTag("EnemyBody"))
                    {
                        Debug.Log("hit enemy");  
                    }
                    else if (hit.collider.CompareTag("MoveZone"))
                    {
                        Debug.Log("hit");
                        float xPos = hit.collider.transform.position.x;
                        newPosition = new Vector3(xPos, transform.position.y, -1);
                        transform.position = newPosition;
                        timeOfNextTP = Time.realtimeSinceStartup + tpCooldown;
                        gameManager.changeTurn();
                    }
                }
            }
            
            
        }
        int timeTillNextTP = (int)(timeOfNextTP - Time.realtimeSinceStartup);
        if (timeTillNextTP < 0)
        {
            timeTillNextTP = 0;
        }
        gameManager.changeTPCooldown(timeTillNextTP);
    }
}
