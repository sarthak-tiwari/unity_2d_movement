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

    private Dictionary<string, string> getParameterStack(string command)
    {
        Dictionary<string, string> parameterStack = new Dictionary<string, string>();
        Debug.Log(command);
        if(command[0] == '[')
        {
            string[] parameters = (command.Substring(1, command.IndexOf(']')-1)).Split(':');
            foreach(string param in parameters){
                if(param != "")
                {
                    Debug.Log("Param: \'" + param + "\'");
                    string datatype = param.Split(' ')[0].Trim();
                    string value = param.Split(' ')[1].Trim();
                    parameterStack.Add(datatype, value);
                }
            }
        }
        if (parameterStack.Count > 0)
            return parameterStack;

        return null;
    }

    private string getFunctionName(string command)
    {
        string functionName = command.Substring(command.IndexOf(']') + 2).Trim();

        return functionName;
    }

    private void ExecuteCommand()
    {
        if(commandQueue.Count != 0)
        {
            string command = (string)commandQueue[0];

            string functionName = getFunctionName(command);
            Dictionary<string, string> parameterStack = getParameterStack(command);

            switch(functionName)
            {
                case "move_bot":
                    if (movCon.ReadyToMove())
                    {
                        commandQueue.RemoveAt(0);

                        string paramDirection;
                        string paramMagnitude;

                        if (!(parameterStack.TryGetValue("DIRECTION", out paramDirection)))
                            paramDirection = "";

                        if (!(parameterStack.TryGetValue("MAGNITUDE", out paramMagnitude)))
                            paramMagnitude = "1";

                            movCon.MoveBot(direction: paramDirection, magnitude: System.Convert.ToInt32(paramMagnitude));
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
