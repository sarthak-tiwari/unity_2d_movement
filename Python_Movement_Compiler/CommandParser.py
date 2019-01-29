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
        for token in tokens:
            tokenInformation = TokenManager.getTokenInformation(token[0], token[1])
            if(tokenInformation.token_type != TokenType.UNDEFINED):
                parsed_result += str(tokenInformation.token_translation) + ' '

        return parsed_result
        
