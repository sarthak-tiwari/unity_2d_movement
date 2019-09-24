# class to handle all the information related to a single conversation frame
class Frame:

    def __init__(self):
        self.intent = ''
        self.entities = []
        self.isComplete = False

        self.__function = ''
        self.__requiredParameters = []
        self.__optionParameters = []
    
    def update(self, newIntent, newEntities):

        #TODO: Will update the frame based on new information

        # If intent is different, load new parameters
        if (newIntent != '' and newIntent != self.intent):

            self.intent = newIntent
            self.entities = newEntities
            

        print('Frame Updated')

    def getMissingEntities(self):

        #TODO: Will return the missing entities
        return []