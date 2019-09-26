# Class which checks whether the given function has necessary information in
# parameter stack

from Common.Constants import DataType
from Common.Constants import TokenType

class FunctionDataManager:

    def __init__(self):
        self.functionDict = {}

    def loadFunctionData(self):

        with open('D:\\Unity\\Chat_Bot\\Function_Data_Module\\FunctionData.txt', 'r') as functionDataFile:
            functionData = functionDataFile.read()
        
        datatypes = set(item.value for item in DataType)

        lookingForFunctionName = True
        functionName = ""
        lookingForMandatoryParameter = False
        mandatoryParameters = []
        lookingForOptionalParameter = False
        optionalParameters = []

        for line in functionData.splitlines():
            
            if(lookingForFunctionName):
                if(line == "M:"):
                    lookingForFunctionName = False
                    lookingForMandatoryParameter = True
                    continue

                if(line.endswith(':')):
                    functionName = line[:line.index(':')]
                    continue

                print("File Corrupted ! Function Name Error !")
                break

            if(lookingForMandatoryParameter):
                if(line == "O:"):
                    lookingForMandatoryParameter = False
                    lookingForOptionalParameter = True
                    continue

                if(line in datatypes):
                    mandatoryParameters.append(line)
                    continue

                print("File Corrupted ! Mandatory Parameter Error !")
                break

            if(lookingForOptionalParameter):
                if(line == 'END:'):
                    lookingForOptionalParameter = False
                    lookingForFunctionName = True
                    parameters = {'MANDATORY':mandatoryParameters,
                                  'OPTIONAL': optionalParameters}
                    self.functionDict[functionName] = parameters
                    functionName = ''
                    mandatoryParameters = []
                    optionalParameters = []
                    continue

                if(line in datatypes):
                    optionalParameters.append(line)
                    continue

                print("File Corrupted ! Optional Parameter Error !")
                break

        #for function in FunctionDataManager.functionDict:
        #    print("Function: " + function)
        #    mP = (FunctionDataManager.functionDict[function])['MANDATORY']
        #    print("Mandatory Parameters: " + str(mP))
        #    print("Optional Parameters: " + str(FunctionDataManager.functionDict[function]['OPTIONAL']))

    def getMandatoryParameters(self, functionName):

        if(len(self.functionDict) == 0):
            raise Exception("Function Dictionary Not Loaded !")

        if(functionName in self.functionDict):
            return (self.functionDict[functionName])['MANDATORY']
        else:
            return None

    def getOptionalParameters(self, functionName):

        if(len(self.functionDict) == 0):
            raise Exception("Function Dictionary Not Loaded !")

        if(functionName in self.functionDict):
            return (self.functionDict[functionName])['OPTIONAL']
        else:
            return None

        

    def checkMissingData(self, functionNames, parameterStack):

        if(len(self.functionDict) == 0):
            self.loadFunctionData()

        for functionName in functionNames:
            if(functionName in self.functionDict):

                requiredParameterStack = self.functionDict[functionName]
                requiredMandatoryParameters = requiredParameterStack['MANDATORY']

                missingParameters = []
                for parameter in requiredMandatoryParameters:
                    if(not(parameter in parameterStack)):
                        missingParameters.append(parameter)

                return missingParameters

            else:
                return (TokenType.FUNCTION, functionName)
