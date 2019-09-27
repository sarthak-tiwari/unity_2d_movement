from Frame import Frame
from Common.Constants import DataType

# class to handle all dialogue related methods and properties
class DialogueManager:

    # Method to initiate the system-user conversation
    def showWelcomeMessage(self):
        # method to show the welcome message

        # TODO: Customize and randomize the welcome message

        print('Hi !')


    # Method to ask the user for missing entities
    def askUserForMissingInformation(self, frame):

        missingEntities = frame.getMissingEntities()

        for entity in missingEntities:
            
            if (DataType(entity) == DataType.DIRECTION):
                print('Which way ?')
            elif (DataType(entity) == DataType.MAGNITUDE):
                print('How much ?')
