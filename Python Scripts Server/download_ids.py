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
import os, shutil

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
name = 'data.json'
place = '/' + name

file_id = "1YAWR9UBtTI7WNEVletdVz1Ue98vn4DRB"
request = service.files().get_media(fileId=file_id)
fh = io.BytesIO()
file_io_base = open(name,'wb')
downloader = MediaIoBaseDownload(file_io_base, request)
done = False
while done is False:
    status, done = downloader.next_chunk()
    print("Download %d%%." % int(status.progress() * 100))
file_io_base.close()








folders = [ r'C:\Local Disk D\Project\Images', r'C:\Local Disk D\Project\Images2', r'C:\Local Disk D\Project\Output Data']
for folder in folders:   
    for filename in os.listdir(folder):
        file_path = os.path.join(folder, filename)
        try:
            if os.path.isfile(file_path) or os.path.islink(file_path):
                os.unlink(file_path)
            elif os.path.isdir(file_path):
                shutil.rmtree(file_path)
        except Exception as e:
            print('Failed to delete %s. Reason: %s' % (file_path, e))