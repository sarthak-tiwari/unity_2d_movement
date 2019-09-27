from Function_Data_Module.FunctionDataManager import FunctionDataManager

# class to handle all the information related to a single conversation frame


class Frame:

    def __init__(self, functionDataManager):
        self.intent = ''
        self.entities = {}
        self.isComplete = False

        self.__function = ''
        self.__requiredParameters = []
        self.__optionalParameters = []

        self.__fnctn_data_mngr = functionDataManager

    def update(self, newIntent, newEntities):

        # If intent is different, load new parameters
        if (newIntent != '' and newIntent != self.intent):

            self.intent = newIntent
            self.entities = newEntities

            self.__function = newIntent

            self.__requiredParameters = self.__fnctn_data_mngr.getMandatoryParameters(
                self.__function)
            self.__optionalParameters = self.__fnctn_data_mngr.getOptionalParameters(
                self.__function)

            self._checkCompleteness()

        elif (newIntent == '' or newIntent == self.intent):

                for newEntity in newEntities:
                    self.entities[newEntity] = newEntities[newEntity]

                self._checkCompleteness()

    def _checkCompleteness(self):

        if (self.__function == ''):
            self.isComplete = False
            return

        for param in self.__requiredParameters:
            if (param not in self.entities):
                self.isComplete = False
                return

        self.isComplete = True

    def getMissingEntities(self):

        missingEntities = []

        for param in self.__requiredParameters:
            if (param not in self.entities):
                missingEntities.append(param)

        return missingEntities

    # Method to create an execution command
    def getExecutionCommand(self):

        #TODO: Creates an execution command based on the frame passed

        commandHeader = 'EXEC:'
        parameterStackStr = '['

        for entity in self.entities:
            parameterStackStr += entity + ' '
            parameterStackStr += self.entities[entity] + ':'

        parameterStackStr += ']:'

        command = commandHeader + parameterStackStr + self.__function
        return command
