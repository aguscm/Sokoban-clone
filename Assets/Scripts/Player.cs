using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public KeyCode MoveRightButton;
    public KeyCode MoveLeftButton;
    public KeyCode MoveUpButton;
    public KeyCode MoveDownButton;
    private int movementX;
    private int movementY;
    public float speed = 2f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(MoveLeftButton))
        {
            MovementX(false);

        }
        else if (Input.GetKey(MoveRightButton))
        {
            MovementX(true);
        }else
        {
            movementX = 0;
        }

        if (Input.GetKey(MoveDownButton))
        {
            MovementY(false);

        }
        else if (Input.GetKey(MoveUpButton))
        {
            MovementY(true);
        }else
        {
            movementY = 0;
        }

        rb.velocity = new Vector2(movementX * speed, movementY * speed);
        var currentPosition = rb.position;
        //rb.position = Vector2.MoveTowards(currentPosition, new Vector2(currentPosition.x + movementX, currentPosition.y + movementY), Time.deltaTime);
    }

    public void MovementX(bool direction)
    {
        movementX = direction ? 1 : -1;
        
    }

    public void MovementY(bool direction)
    {
        movementY = direction ? 1 : -1;
    }
}
