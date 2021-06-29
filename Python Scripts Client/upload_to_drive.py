import os.path
from googleapiclient.discovery import build
from google_auth_oauthlib.flow import InstalledAppFlow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
import io
from apiclient.http import MediaIoBaseDownload
from apiclient.http import MediaFileUpload
import pickle
import time
import json
from os import path

SCOPES = ['https://www.googleapis.com/auth/drive']
creds = None
# The file token.json stores the user's access and refresh tokens, and is
# created automatically when the authorization flow completes for the first
# time.
if os.path.exists('token.json'):
    creds = Credentials.from_authorized_user_file('token.json', SCOPES)
# If there are no (valid) credentials available, let the user log in.
if not creds or not creds.valid:
    if creds and creds.expired and creds.refresh_token:
        creds.refresh(Request())
    else:
        flow = InstalledAppFlow.from_client_secrets_file(
            'credentials.json', SCOPES)
        creds = flow.run_local_server(port=0)
    # Save the credentials for the next run
    with open('token.json', 'w') as token:
        token.write(creds.to_json())

api_key = '467124070709-gaptkp5nbr3dfh3gjugehukgicjqejbo.apps.googleusercontent.com'
service = build('drive', 'v3', credentials=creds)    
    
count = 0
a_file = open("data.json", "r")
ids = a_file.read()
ids = json.loads(ids)
a_file.close()
while(1):
    name = str(count+1) +'.jpg'
    place = 'C:/Users/VR-LAB 1228/AppData/LocalLow/DefaultCompany/test 16/Screenshots/'+name
    if(path.exists(place)):
        time.sleep(0.5)
        file_metadata = {'name': name, 'id':ids['ids'][int(count)]}
        try:
            media = MediaFileUpload(place , mimetype='image/jpeg')
            file = service.files().create(body=file_metadata, media_body=media, fields='id').execute()
            print('File ID: %s' % file.get('id'))
        except OSError:
            count-=1
    else:
        count-=1
    count+=1
    time.sleep(1)