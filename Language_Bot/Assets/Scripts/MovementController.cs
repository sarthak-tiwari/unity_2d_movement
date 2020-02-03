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

    private bool carrying;
    private Transform carriedObject;

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
        if (ReadyToMoveOrTurn())
        {
            pastMovementPosition = FutureMovementPosition;
            pastTurnPosition = FutureTurnPosition;

            if (directControl)
            {
                #region MoveControls_WSAD
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
                #endregion

                #region TurnControls_Arrows
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    TurnBot("lft");
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    TurnBot("rght");
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    TurnBot("bck");
                }
                #endregion

                #region Action_Keys
                if (Input.GetKey(KeyCode.Space))
                {
                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(transform.position, transform.forward, out hit, 6, 1, QueryTriggerInteraction.Collide))
                    {
                        if((((BoxController)hit.transform.gameObject.GetComponent("BoxController")).GetObjectColor() == "blue"))
                        {
                            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue, 2f);
                        }
                        else if ((((BoxController)hit.transform.gameObject.GetComponent("BoxController")).GetObjectColor() == "red"))
                        {
                            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red, 2f);
                        }
                        Debug.Log("Did Hit");
                    }
                }

                if (Input.GetKey(KeyCode.E))
                {
                    if (!carrying)
                        PickObject();
                }

                if (Input.GetKey(KeyCode.R))
                {
                    if (carrying)
                        DropObject();
                }

                #endregion
            }
        }

        if (!collided)
        {
            if (NeedToMove())
            {
                transform.position = Vector3.MoveTowards(transform.position, FutureMovementPosition, Time.deltaTime * moveSpeed);

                if (carrying)
                    carriedObject.position = new Vector3(transform.position.x, transform.position.y + carriedObject.localScale.y + 1f, transform.position.z);
            }
            else if (NeedToTurn())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, FutureTurnPosition, Time.deltaTime * turnSpeed);
            }
        }
        else
        {
            if (NeedToMove())
            {
                transform.position = Vector3.MoveTowards(transform.position, pastMovementPosition, Time.deltaTime * moveSpeed);

                if (carrying)
                    carriedObject.position = new Vector3(transform.position.x, transform.position.y + carriedObject.localScale.y + 1f, transform.position.z);
            }
            else if(NeedToTurn())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, pastTurnPosition, Time.deltaTime * turnSpeed);
            }
        }
    }

    public bool ReadyToMoveOrTurn()
    {
        return (NeedToMove() || NeedToTurn()) ? false : true;
    }

    private bool NeedToMove()
    {
        return (transform.position == FutureMovementPosition) ? false : true;
    }

    private bool NeedToTurn()
    {
        return (transform.rotation == FutureTurnPosition) ? false : true;
    }

    public void MoveBot(string direction, int magnitude = 1)
    {
        if (ReadyToMoveOrTurn())
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
        if (ReadyToMoveOrTurn())
        {
            Quaternion directionToTurn;
            switch (direction)
            {
                //case "fwd":
                //    directionToTurn = Quaternion.Euler(0,360,0);
                //    break;
                case "bck":
                    directionToTurn = Quaternion.Euler(0, 180f, 0);
                    break;
                case "rght":
                    directionToTurn = Quaternion.Euler(0, 90, 0);
                    break;
                case "lft":
                    directionToTurn = Quaternion.Euler(0, -90, 0);
                    break;
                default:
                    directionToTurn = Quaternion.identity;
                    break;
            }

            FutureTurnPosition = directionToTurn * transform.rotation;
        }
    }

    public bool PickObject()
    {

        if (carrying)
            return false;

        Transform objectToMove;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1, 1, QueryTriggerInteraction.Collide))
        {
            if (hit.transform.gameObject.CompareTag("Box"))
            {
                objectToMove = hit.transform;
                // add check for boolean can be moved
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        objectToMove.position = new Vector3(transform.position.x, transform.position.y + objectToMove.localScale.y + 1f, transform.position.z);
        carriedObject = objectToMove;
        carrying = true;
        return true;
    }

    public bool DropObject()
    {
        if(carrying)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2, 1, QueryTriggerInteraction.Collide))
            {
                return false;
            }

            carriedObject.position = transform.position + transform.forward + new Vector3(0, 0.75f, 0); // use object height here

            carriedObject = null;
            carrying = false;
            return true;
        }

        return false;
    }
}
