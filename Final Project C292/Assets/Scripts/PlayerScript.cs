using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();   
    }

    void checkMove()
    {
        Vector3 newPosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
        if (Input.GetMouseButtonDown(0))
        {
            move = true;
        }
        if (move == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed);
        }
        if (transform.position.x == newPosition.x)
        {
            move = false;
        }
    }
}
