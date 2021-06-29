import os.path
from googleapiclient.discovery import build
from google_auth_oauthlib.flow import InstalledAppFlow
from google.auth.transport.requests import Request
from google.oauth2.credentials import Credentials
import io
from apiclient.http import MediaIoBaseDownload
from apiclient.http import MediaFileUpload
import json
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
idss = service.files().generateIds(count = 8, space = 'drive').execute()

a_file = open("data.json", "w")
json.dump(idss, a_file)
a_file.close()




service = build('drive', 'v2', credentials=creds)   
file_id = "1YAWR9UBtTI7WNEVletdVz1Ue98vn4DRB"
name = 'data.json'
place = 'D:/Vijit/Python Scripts/'+ name
file_metadata = {'name': name, 'id':file_id}
#media = MediaFileUpload(place , mimetype='application/json')
file = service.files().get(fileId=file_id).execute()
media_body = MediaFileUpload( place, mimetype='application/json')

#file = service.files().create(body=file_metadata, media_body=media, fields='id').execute()
updated_file = service.files().update(fileId=file_id, body=file, newRevision=False, media_body=media_body).execute()
print('File ID: %s' % file.get('id'))




folders = [ r'C:\Users\VR-LAB 1228\AppData\LocalLow\DefaultCompany\test 16\Screenshots', r'D:\Vijit\Python Scripts\Output Data', r'D:\Vijit\Python Scripts\Output Data2']
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
