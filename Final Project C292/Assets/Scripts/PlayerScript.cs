using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector3 newPosition;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();

    }
    private void FixedUpdate()
    {

        
    }

    void checkMove()
    {
        if (!isMoving)
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
                        GameObject enemy = enemyCollider.transform.parent.gameObject;
                        Destroy(enemy);
                    }
                    else if (hit.collider.CompareTag("MoveZone"))
                    {
                        Debug.Log("hit");
                        float xPos = hit.collider.transform.position.x;
                        newPosition = new Vector2 (xPos, transform.position.y);
                        isMoving = true;
                    }
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed);
        if (transform.position == newPosition)
        {
            isMoving = false;
        }
    }
}
