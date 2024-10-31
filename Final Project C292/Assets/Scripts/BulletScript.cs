using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    GameManager gameManager;
    public int side;
    GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        self = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (side == 0)
        {
            transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
        if (side == 1) 
        {
            transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit player");
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(self);
            gameManager.GameOver();   
        }
    }
}
