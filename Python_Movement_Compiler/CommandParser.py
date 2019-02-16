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
        parameterStack_str = '['
        parameterStack = []
        functionStack_str = ''
        functionStack = []

        for token in tokens:
            tokenInformation = TokenManager.getTokenInformation(token[0], token[1])

            if(tokenInformation.token_type == TokenType.PARAMETER):
                parameterStack_str += (str(tokenInformation.token_translation.name)
                                    + " " + str(tokenInformation.token_value) + ":")
                parameterStack.append(tokenInformation.token_translation.name)
            
            if(tokenInformation.token_type == TokenType.FUNCTION):
                functionStack_str = str(tokenInformation.token_value)
                functionStack.append(tokenInformation.token_value)

        parameterStack_str += " ]"

        parsed_result = parameterStack_str + ":" + functionStack_str

        result = (parsed_result, parameterStack, functionStack)

        return result
        
