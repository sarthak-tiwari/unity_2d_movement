# Class to manage all the grammar token information

import collections
from enum import Enum

# defining return format for getTokenInformation method
TokenInformationData = collections.namedtuple("TokenInformationData",
                                              "token_type, token_value, token_translation")

# declaring token_type valid values
class TokenType(Enum):
    FUNCTION = 'FUNCTION'
    PARAMETER = 'PARAMETER'
    UNDEFINED = 'UNDEFINED'


# main class TokenManager
class TokenManager:

    FUNCTION_DICTIONARY = { 'MOVE': 'move_bot' }
    DIRECTION_WORDS_DICTIONARY = { 'FORWARD': 'fwd', 'BACK': 'bck', 'LEFT': 'lft', 'RIGHT': 'rght'}

    POS_TYPE_TO_SYSTEM_TYPE_DICTIONARY = { 'NN': 'FUNCTION_DICTIONARY',
                                              'VB': 'FUNCTION_DICTIONARY',
                                              'RB': 'DIRECTION_WORDS_DICTIONARY' }
    
    @staticmethod
    def getTokenInformation(token, tokenPOSType):
        # gets a token and returns its translation back in the TokenInformationData format

        token_type = None
        token_translation = None

        try:
            dictionary_name = TokenManager.POS_TYPE_TO_SYSTEM_TYPE_DICTIONARY[tokenPOSType.upper()]
        except Exception as err:
            dictionary_name = None

        if(dictionary_name is None):
                    tokenInformation = TokenInformationData(token_type=TokenType.UNDEFINED,
                                                token_value=token,
                                                token_translation=None)
        else:

            if(dictionary_name == 'FUNCTION_DICTIONARY'):
                token_type = TokenType.FUNCTION
            else:
                token_type = TokenType.PARAMETER

            dictionary_to_use = getattr(TokenManager, dictionary_name)

            try:
                token_translation = dictionary_to_use[token.upper()]
            except Exception as err:
                token_translation = None

            tokenInformation = TokenInformationData(token_type = token_type,
                                                token_value = token,
                                                token_translation = token_translation)
        return tokenInformation