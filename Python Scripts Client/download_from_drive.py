import os.path
from googleapiclient.discovery import build
from google_auth_oauthlib.flow import InstalledAppFlow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
import io
from apiclient.http import MediaIoBaseDownload
from googleapiclient.errors import HttpError 
import json
import os
import time
from shutil import copyfile
from playsound import playsound

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

arr = [1 for i in range(199)]
count2 = 0
a_file = open("data.json", "r")
ids = a_file.read()
ids = json.loads(ids)
a_file.close()
count5 = 0
while(1):
    count5 += 1
    name2 = str(count2 + 1) + '.json'
    place2 = 'Output Data/' + name2
    place3 = 'Output Data2/' + name2
    file_id = ids['ids'][int(count2 + 4)]
    request = service.files().get_media(fileId=file_id)
    fh = io.BytesIO()
    file_io_base = open(place3,'wb')
    downloader = MediaIoBaseDownload(file_io_base, request)
    done = False
    while done is False:
        try:
            status, done = downloader.next_chunk()
            print("Download %d%%." % int(status.progress() * 100))
            copyfile(place3, place2)
        except HttpError:
            #print('erroe')
            file_io_base.close()
            os.remove(place3)
            arr[count2] = 2
            done = True;
            count2 -= 1
    file_io_base.close()
    count2 += 1
    #if(count5 == 20):
     #   playsound(r'C:\Users\VR-LAB 1228\Documents\Sound recordings\Recording2.wav')
    time.sleep(0.5)