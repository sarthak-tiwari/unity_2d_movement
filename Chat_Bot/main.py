from NLU_Module import Intent_Entity_Extraction
from DialogueManager import DialogueManager
from Function_Data_Module import FunctionDataManager
from Frame import Frame

# Module to launch and host the interface of the chat part of the bot

if __name__ == '__main__':

    currentFrame = None

    dlg_mngr = DialogueManager()

    fnctn_data_mngr = FunctionDataManager()
    fnctn_data_mngr.loadFunctionData()

    dlg_mngr.showWelcomeMessage()

    while (True):

        text = input()

        (intent, entities) = Intent_Entity_Extraction.get_intent_entity_from_server(text)

        print(intent)
        print('--------')
        print(entities)

        if (currentFrame is None):
            currentFrame = Frame(fnctn_data_mngr)

        currentFrame.update(intent, entities)
        
        if (currentFrame.isComplete):
            
            print(currentFrame.getExecutionCommand())

            currentFrame = None

        else:
            
            dlg_mngr.askUserForMissingInformation(currentFrame)
