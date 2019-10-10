using UnityEngine;

public class MovementController : MonoBehaviour
{

    public float unitMovementHorizontal;
    public float unitMovementVertical;
    public float speed;
    public bool directControl;

    private Vector3 unitVectorMovementHorizontal;
    private Vector3 unitVectorMovementVertical;
    private Vector3 pastPosition;
    private bool collided;

    public Vector3 FuturePosition { get; set; }

    // Use this for initialization
    void Start()
    {
        FuturePosition = transform.position;
        unitVectorMovementHorizontal = new Vector3(unitMovementHorizontal, 0, 0);
        unitVectorMovementVertical = new Vector3(0, 0, unitMovementVertical);
    }

    void OnTriggerEnter(Collider other)
    {
        collided = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        FuturePosition = pastPosition;
        collided = false;
    }

    void FixedUpdate()
    {

        if (transform.position == FuturePosition)
        {
            pastPosition = FuturePosition;
            if (directControl)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    FuturePosition += (unitVectorMovementHorizontal * -1);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    FuturePosition += unitVectorMovementHorizontal;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    FuturePosition += unitVectorMovementVertical;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    FuturePosition += (unitVectorMovementVertical * -1);
                }
            }
        }

        if (!collided)
        {
            transform.position = Vector3.MoveTowards(transform.position, FuturePosition, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pastPosition, Time.deltaTime * speed);
        }
    }

    public bool ReadyToMove()
    {
        if (transform.position == FuturePosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveBot(string direction, int magnitude = 1)
    {
        if (ReadyToMove())
        {
            Vector3 directionToMove;
            switch (direction)
            {
                case "fwd":
                    directionToMove = unitVectorMovementVertical;
                    break;
                case "bck":
                    directionToMove = (unitVectorMovementVertical * -1);
                    break;
                case "rght":
                    directionToMove = unitVectorMovementHorizontal;
                    break;
                case "lft":
                    directionToMove = (unitVectorMovementHorizontal * -1);
                    break;
                default:
                    directionToMove = unitVectorMovementVertical;
                    break;
            }

            FuturePosition += directionToMove * magnitude;
        }
    }
}
