import os.path
from googleapiclient.discovery import build
from google_auth_oauthlib.flow import InstalledAppFlow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
import io
from apiclient.http import MediaIoBaseDownload
import json
import time
import os
from os import path
from googleapiclient.errors import HttpError
from shutil import copyfile

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
count3 = 0
a_file = open("data.json", "r")
ids = a_file.read()
ids = json.loads(ids)
a_file.close()
arr = [True for i in range(199)]

while(1):

#file_id = "1UvRUeyr8EVj7gdb4K0wGDfjjkhgId3fr"
    name = str(count3+1) + '.jpg'
    place = 'Images/' + name
    place2 = 'Images2/' + name
    file_id = ids['ids'][int(count3)]
    request = service.files().get_media(fileId=file_id)
    fh = io.BytesIO()
    file_io_base = open(place2,'wb')
    downloader = MediaIoBaseDownload(file_io_base, request)
    done = False
    while done is False:
        try:
            status, done = downloader.next_chunk()
            print("Download %d%%." % int(status.progress() * 100))
        except HttpError:
            #print('error')
            file_io_base.close()
            os.remove(place2)
            arr[count3] = False
            done = True
            count3-=1
    if(arr[count3+1]):
        copyfile(place2, place) 
    file_io_base.close()
    count3+=1
    time.sleep(0.5)