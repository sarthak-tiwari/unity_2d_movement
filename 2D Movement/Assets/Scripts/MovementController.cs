using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float unitMovementHorizontal;
    public float unitMovementVertical;
    public float speed;
    public bool directControl;

    private Vector3 unitVectorMovementHorizontal;
    private Vector3 unitVectorMovementVertical;
    private Vector3 futurePosition;
    private Vector3 pastPosition;
    private bool collided;

    public Vector3 FuturePosition
    {
        get
        {
            return futurePosition;
        }

        set
        {
            futurePosition = value;
        }
    }

    // Use this for initialization
    void Start () {
        futurePosition = transform.position;
        unitVectorMovementHorizontal = new Vector3(unitMovementHorizontal, 0, 0);
        unitVectorMovementVertical = new Vector3(0, unitMovementVertical, 0);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        collided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        futurePosition = pastPosition;
        collided = false;
    }

    void FixedUpdate () {

        if(transform.position == futurePosition)
        {
            pastPosition = futurePosition;
            if (directControl)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    futurePosition += (unitVectorMovementHorizontal * -1);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    futurePosition += unitVectorMovementHorizontal;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    futurePosition += unitVectorMovementVertical;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    futurePosition += (unitVectorMovementVertical * -1);
                }
            }
        }

        if (!collided)
        {
            transform.position = Vector3.MoveTowards(transform.position, futurePosition, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pastPosition, Time.deltaTime * speed);
        }
    }

    public bool ReadyToMove()
    {
        if (transform.position == futurePosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveBot(string direction)
    {
        if (ReadyToMove())
        {
            switch (direction)
            {
                case "fwd":
                    futurePosition += unitVectorMovementVertical;
                    break;
                case "bck":
                    futurePosition += (unitVectorMovementVertical * -1);
                    break;
                case "rght":
                    futurePosition += unitVectorMovementHorizontal;
                    break;
                case "lft":
                    futurePosition += (unitVectorMovementHorizontal * -1);
                    break;
                default:
                    break;
            }
        }
    }
}
