# Class which takes command and converts it into core methods

from CommandParser import CommandParser
from FunctionDataManager import FunctionDataManager
from Constants import DataType
import sys

class Planner:

    @staticmethod
    def returnToUnity(parsed_result):
        # uses command line arguments to make the command and pass to parser

        print(parsed_result)

    @staticmethod
    def launch(command):

        (result, parameterStack, functionStack) = CommandParser.parseCommand(command)

        missingData = FunctionDataManager.checkMissingData(functionStack, parameterStack)

        if(len(missingData) > 0):
            print('ERROR:')

        # function not found
        if(type(missingData) == tuple):
            print("Function Not Found: " + missingData[1])
        else:
            for param in missingData:
                if(param == DataType.DIRECTION.name):
                    print("Which Way ? Direction missing !")
                elif(param == str(DataType.MAGNITUDE.name)):
                    print("How much ? Magnitude missing !")


        Planner.returnToUnity(result)


# Main
command = sys.argv[1]
# command = 'move 3 units'

Planner.launch(command)
