# Class which takes command and converts it into core methods

from CommandParser import CommandParser
import sys

class Planner:

    @staticmethod
    def returnToUnity(parsed_result):
        # uses command line arguments to make the command and pass to parser

        print(parsed_result)

# Main
command = sys.argv[1]
# command = 'move left'
parsed_result = CommandParser.parseCommand(command)
Planner.returnToUnity(parsed_result)
