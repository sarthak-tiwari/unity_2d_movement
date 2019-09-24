import json
import http
import requests

# Class to handle interaction with the nlu server to fetch intent and entities
# for a given text

class Intent_Entity_Extraction:

    SERVER_URL = "http://localhost:5005/model/parse"
    HEADER = {"Content-Type": "application/json"}
    INTENT_CONFIDENCE_THRESHOLD = 0.7

    # method that passes the message to server and returns the most probable
    # intent along with associated entities.
    @staticmethod
    def get_intent_entity_from_server(message):

        intent = ""
        entities = []

        data = {"text": message}

        try:
            resp = requests.post(Intent_Entity_Extraction.SERVER_URL, headers = Intent_Entity_Extraction.HEADER, data = json.dumps(data))
        except:
            print("Something went wrong with server !")
            return (intent, entities)

        #error - Need to update this check
        #if resp.status_code != 200:
        #    raise ApiError('GET /tasks/ {}'.format(resp.status_code))

        highestIntent = resp.json().get('intent', '')
        if (highestIntent != ''):
            if (highestIntent.get('confidence') >= Intent_Entity_Extraction.INTENT_CONFIDENCE_THRESHOLD ):
                intent = highestIntent.get('name')

        entities = resp.json().get('entities', [])

        return (intent, entities)