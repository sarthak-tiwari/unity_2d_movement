using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    public MovementController movCon;

    private ArrayList commandQueue = new ArrayList();
    
    public void AddCommand(string command)
    {
        commandQueue.Add(command);
    }

    private void ExecuteCommand()
    {
        if(commandQueue.Count != 0)
        {
            string command = (string)commandQueue[0];
            string[] splittedCommand = command.Split(' ');

            switch(splittedCommand[0])
            {
                case "move_bot":
                    if (movCon.ReadyToMove())
                    {
                        commandQueue.RemoveAt(0);
                        if (splittedCommand.Length > 3)
                            movCon.MoveBot(splittedCommand[1], System.Convert.ToInt32(splittedCommand[2]));
                        else
                            movCon.MoveBot(splittedCommand[1]);
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
        ExecuteCommand();
    }
}
