from enum import Enum

class DataType(Enum):
    DIRECTION = 'DIRECTION'
    MAGNITUDE = 'MAGNITUDE'

# declaring token_type valid values
class TokenType(Enum):
    FUNCTION = 'FUNCTION'
    PARAMETER = 'PARAMETER'
    UNDEFINED = 'UNDEFINED'