import face_recognition
image = face_recognition.load_image_file("1.png")
face_locations = face_recognition.face_locations(image)
face_landmarks_list = face_recognition.face_landmarks(image)
print( ' There are {0} faces in the image ' .format(len(face_locations)))