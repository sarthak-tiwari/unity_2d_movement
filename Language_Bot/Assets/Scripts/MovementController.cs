using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float unitMovementHorizontal;
    public float unitMovementVertical;
    public float moveSpeed;
    public float turnSpeed;
    public bool directControl;

    private Vector3 pastMovementPosition;
    private Quaternion pastTurnPosition;
    private bool collided;

    private Vector3 FutureMovementPosition { get; set; }
    private Quaternion FutureTurnPosition { get; set; }

    // Use this for initialization
    void Start()
    {
        FutureMovementPosition = transform.position;
        FutureTurnPosition = transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        collided = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        FutureMovementPosition = pastMovementPosition;
        FutureTurnPosition = pastTurnPosition;
        collided = false;
    }

    void FixedUpdate()
    {
        if (transform.position == FutureMovementPosition)
        {
            pastMovementPosition = FutureMovementPosition;
            if (directControl)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    FutureMovementPosition += (transform.right * -1 * unitMovementHorizontal);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    FutureMovementPosition += transform.right * unitMovementHorizontal;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    FutureMovementPosition += transform.forward * unitMovementVertical;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    FutureMovementPosition += (transform.forward * -1 * unitMovementVertical);
                }
            }
        }

        if (transform.rotation == FutureTurnPosition)
        {
            pastTurnPosition = FutureTurnPosition;
            if (directControl)
            {
                if(Input.GetKey(KeyCode.RightArrow))
                {
                    TurnBot("rght");
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    TurnBot("lft");
                }
            }
        }

        if (!collided)
        {
            transform.position = Vector3.MoveTowards(transform.position, FutureMovementPosition, Time.deltaTime * moveSpeed);
            if (ReadyToMove())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, FutureTurnPosition, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pastMovementPosition, Time.deltaTime * moveSpeed);
            if (ReadyToMove())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, pastTurnPosition, Time.deltaTime * turnSpeed);
            }
        }
    }

    public bool ReadyToMove()
    {
        if (transform.position == FutureMovementPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ReadyToTurn()
    {
        if (ReadyToMove() && transform.rotation == FutureTurnPosition)
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
                    directionToMove = transform.forward * unitMovementVertical;
                    break;
                case "bck":
                    directionToMove = (transform.forward * unitMovementVertical * -1);
                    break;
                case "rght":
                    directionToMove = transform.right * unitMovementHorizontal;
                    break;
                case "lft":
                    directionToMove = (transform.right * unitMovementHorizontal * -1);
                    break;
                default:
                    directionToMove = transform.forward * unitMovementVertical;
                    break;
            }

            FutureMovementPosition += directionToMove * magnitude;
        }
    }

    public void TurnBot(string direction)
    {
        if (ReadyToTurn())
        {
            Debug.Log(direction);
            Quaternion directionToTurn;
            switch (direction)
            {
                case "fwd":
                    directionToTurn = Quaternion.Euler(0,0,0);
                    break;
                case "bck":
                    directionToTurn = Quaternion.Euler(0, 180, 0);
                    break;
                case "rght":
                    directionToTurn = Quaternion.Euler(0, 90, 0);
                    break;
                case "lft":
                    directionToTurn = Quaternion.Euler(0, -90, 0);
                    break;
                default:
                    directionToTurn = Quaternion.Euler(0, 0, 0);
                    break;
            }

            FutureTurnPosition *= directionToTurn;
        }
    }
}
