﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PythonManager : MonoBehaviour
{
    public ChatController chatController;
    public CommandExecutor commExec;

    private string COMPILER_PATH = "D:\\Unity\\Python_Movement_Compiler\\Planner.py";

    public void testCompiler(string command)
    {
        using (Process otherProcess = new Process())
        {
            otherProcess.StartInfo.FileName = "python";
            otherProcess.StartInfo.Arguments = COMPILER_PATH + " \"" + command + "\"";
            otherProcess.StartInfo.CreateNoWindow = true;
            otherProcess.StartInfo.UseShellExecute = false;
            otherProcess.StartInfo.RedirectStandardInput = true;
            otherProcess.StartInfo.RedirectStandardOutput = true;

            otherProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Prepend line numbers to each line of the output.
                if (!String.IsNullOrEmpty(e.Data))
                {
                    commExec.addCommand(e.Data);
                    chatController.AddCompilerOutput(e.Data);
                    //output.Append("\n[" + lineCount + "]: " + e.Data);
                }
            });

            otherProcess.Start();

            // Asynchronously read the standard output of the spawned process. 
            // This raises OutputDataReceived events for each line of output.
            otherProcess.BeginOutputReadLine();
            otherProcess.WaitForExit();

            otherProcess.Close();
        }


        // Now communicate via streams
        //     otherProcess.StandardOutput
        // and
        //     otherProcess.StandardInput

    }
}
