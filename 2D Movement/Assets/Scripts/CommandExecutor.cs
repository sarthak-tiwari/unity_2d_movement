using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    public MovementController movCon;

    private ArrayList commandQueue = new ArrayList();
    
    public void addCommand(string command)
    {
        commandQueue.Add(command);
    }

    private void executeCommand()
    {
        if(commandQueue.Count != 0)
        {
            string command = (string)commandQueue[0];

            switch(command.Split(' ')[0])
            {
                case "move_bot":
                    if (movCon.ReadyToMove())
                    {
                        commandQueue.RemoveAt(0);
                        movCon.MoveBot(command.Split(' ')[1]);
                    }
                    break;

                default:
                    commandQueue.RemoveAt(0);
                    break;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        executeCommand();
    }
}
