using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PythonManager : MonoBehaviour
{
    public ChatController chatController;
    public CommandExecutor commExec;

    private string COMPILER_PATH = "D:\\Unity\\Chat_Bot\\main.py";

    Process otherProcess = null;

    public void testCompiler(string command)
    {
        if (otherProcess == null)
        {
            otherProcess = new Process();

            otherProcess.StartInfo.FileName = "python";
            otherProcess.StartInfo.Arguments = COMPILER_PATH; // + " \"" + command + "\"";
            otherProcess.StartInfo.CreateNoWindow = true;
            otherProcess.StartInfo.UseShellExecute = false;
            otherProcess.StartInfo.RedirectStandardInput = true;
            otherProcess.StartInfo.RedirectStandardOutput = true;

            otherProcess.Start();
        }

        otherProcess.StandardInput.WriteLine(command);

        string resp = otherProcess.StandardOutput.ReadLine();
        if (!String.IsNullOrEmpty(resp))
        {
            if (resp.Substring(0, 5) == "EXEC:")
            {
                commExec.AddCommand(resp.Substring(5));
            }
            else
            {
                chatController.AddCompilerOutput(resp);
            }
        }
    }
}
