# Class which handles the parsing of the command entered

import nltk
from TokenManager import *

class CommandParser:

    @staticmethod
    def posTagger(command):
        # function which uses nltk to breakdown the command into tokens and tag pos

        text = nltk.word_tokenize(command)
        tagged_tokens = nltk.pos_tag(text)
        return tagged_tokens


    @staticmethod
    def parseCommand(command):
        #will parse passed command

        tokens = CommandParser.posTagger(command)
        parsed_result = ''
        parameter_stack = '['
        function_stack = ''

        for token in tokens:
            tokenInformation = TokenManager.getTokenInformation(token[0], token[1])

            if(tokenInformation.token_type == TokenType.PARAMETER):
                parameter_stack += (str(tokenInformation.token_translation.name)
                                    + " " + str(tokenInformation.token_value) + ":")
            
            if(tokenInformation.token_type == TokenType.FUNCTION):
                function_stack = str(tokenInformation.token_value)

        parameter_stack += " ]"

        parsed_result = parameter_stack + ":" + function_stack

        return parsed_result
        
