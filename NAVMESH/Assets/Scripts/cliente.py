import requests
import json

url = "http://localhost:8080/"
data = {"adolfo": 1}
response = requests.post(url, json.dumps(data))
print(response)