from NLU_Module import Intent_Entity_Extraction
from DialogueManager import DialogueManager
from Frame import Frame

# Module to launch and host the interface of the chat part of the bot

# Method to create an execution command based on the frame passed
def getExecutionCommand(frame):

    #TODO: Creates an execution command based on the frame passed
    return "EXEC: ... []"


if __name__ == '__main__':

    currentFrame = None

    dlg_mngr = DialogueManager()

    dlg_mngr.showWelcomeMessage()

    while (True):

        text = input()

        (intent, entities) = Intent_Entity_Extraction.get_intent_entity_from_server(text)

        print(intent)
        print('--------')
        print(entities)

        if (currentFrame is None):
            currentFrame = Frame()

        currentFrame.update(intent, entities)
        
        if (currentFrame.isComplete):
            
            print(getExecutionCommand(currentFrame))

            currentFrame = None

        else:
            
            dlg_mngr.askUserForMissingInformation(currentFrame)
