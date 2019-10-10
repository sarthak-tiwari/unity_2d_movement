using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour
{


    public TMP_InputField TMP_ChatInput;

    public TMP_Text TMP_ChatOutput;

    public Scrollbar ChatScrollbar;

    public PythonManager pythonManager;

    void OnEnable()
    {
        TMP_ChatInput.onSubmit.AddListener(AddToChatOutput);
    }

    void OnDisable()
    {
        TMP_ChatInput.onSubmit.RemoveListener(AddToChatOutput);

    }


    void AddToChatOutput(string newText)
    {
        // Clear Input Field
        TMP_ChatInput.text = string.Empty;

        //var timeNow = System.DateTime.Now;

        TMP_ChatOutput.text += "\n>> " + newText + "\n";

        TMP_ChatInput.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;

        pythonManager.testCompiler(newText);

    }

    public void AddCompilerOutput(string newText)
    {
        // Clear Input Field
        TMP_ChatInput.text = string.Empty;

        //var timeNow = System.DateTime.Now;

        TMP_ChatOutput.text += newText + "\n";

        //Don't uncomment, hangs the editor on run
        //TMP_ChatInput.ActivateInputField();

        // Set the scrollbar to the bottom when next text is submitted.
        ChatScrollbar.value = 0;

    }

}
