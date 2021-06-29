import os.path
from googleapiclient.discovery import build
from google_auth_oauthlib.flow import InstalledAppFlow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
import io
from apiclient.http import MediaIoBaseDownload
from apiclient.http import MediaFileUpload
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

service = build('drive', 'v3', credentials=creds)    

count2 = 0
a_file2 = open('data.json', 'r')
ids2 = a_file2.read()
a_file2.close()
ids2 = json.loads(ids2)

while(1):
    name2 = str(count2 + 1) + '.json'
    place2 = 'Output Data/' + name2
    if(path.exists(place2)):
        time.sleep(0.5)
        file_metadata = {'name': name2, 'id': ids2['ids'][int(count2 + 4)]}
        try:
            media = MediaFileUpload(place2 , mimetype='image/jpeg')
            file = service.files().create(body=file_metadata, media_body=media, fields='id').execute()
            print('File ID: %s' % file.get('id'))
        except OSError:
            count2 -= 1
            time.sleep(0.5)
    else:
        count2-=1
    count2 += 1