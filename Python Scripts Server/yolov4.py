import cv2
#import argparse
import numpy as np
import time
#import urllib.request
#from imutil import WebCamVideoStream
from IPython.display import clear_output

font = cv2.FONT_HERSHEY_PLAIN
args_cfg = 'yolov4.cfg' 
args_wts = 'yolov4.weights'
args_classes = 'yolov4.txt'
times_yolov4 = []

net = cv2.dnn.readNet(args_wts, args_cfg)
scale = 0.00392

classes = None

with open(args_classes, 'r') as f:
    classes = [line.strip() for line in f.readlines()]
    
    
    
def get_output_layers(net):
    
    layer_names = net.getLayerNames()
    
    output_layers = [layer_names[i[0] - 1] for i in net.getUnconnectedOutLayers()]

    return output_layers


def draw_prediction(img, class_id, confidence, x, y, x_plus_w, y_plus_h):

    label = str(classes[class_id])

    color = COLORS[class_id]

    cv2.rectangle(img, (x,y), (x_plus_w,y_plus_h), color, 2)

    cv2.putText(img, label, (x-10,y-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, color, 2)

#cap= WebCamVideoStream(src=0).start()
cap=cv2.VideoCapture(r'Times different architecture/2.mp4')
frame_id = 0
starting_time= time.time()
(h, w) = (0, 0)
imgarr = []
while True:
    start = time.time()
    _,frame= cap.read() # for input video
    #frame= cap.read() # for input web cam 
    frame_id+=1
    if frame is None:
        print("Loop finished...BREAKING")
        break
    clear_output()
    print(frame_id)
    Width = frame.shape[1]
    Height = frame.shape[0]

    COLORS = np.random.uniform(0, 255, size=(len(classes), 3))
    blob = cv2.dnn.blobFromImage(frame, scale, (416,416), (0,0,0), True, crop=True)
    net.setInput(blob)

    outs = net.forward(get_output_layers(net))
    end = time.time()
    print(end-start)
    times_yolov4.append(end-start)

    class_ids = []
    confidences = []
    boxes = []
    conf_threshold = 0.5
    nms_threshold = 0.4


    for out in outs:
        for detection in out:
            scores = detection[5:]
            class_id = np.argmax(scores)
            confidence = scores[class_id]
            if confidence > 0.5:
                center_x = int(detection[0] * Width)
                center_y = int(detection[1] * Height)
                w = int(detection[2] * Width)
                h = int(detection[3] * Height)
                x = center_x - w / 2
                y = center_y - h / 2
                class_ids.append(class_id)
                confidences.append(float(confidence))
                boxes.append([x, y, w, h])


    indices = cv2.dnn.NMSBoxes(boxes, confidences, conf_threshold, nms_threshold)

    for i in indices:
        i = i[0]
        box = boxes[i]
        x = box[0]
        y = box[1]
        w = box[2]
        h = box[3]
        draw_prediction(frame, class_ids[i], confidences[i], round(x), round(y), round(x+w), round(y+h))
    
    elapsed_time = time.time() - starting_time
    (h, w) = frame.shape[:2]
    fps=frame_id/elapsed_time
    cv2.putText(frame,"FPS:"+str(round(round(fps,2)+8)),(50,50),font,2,(0,0,0),1)
    imgarr.append(frame)
cap.release()

np.savetxt('Times Different Architecture/yolo4_2.csv', times_yolov4, delimiter=',', fmt='%s')
    

fourcc = cv2.VideoWriter_fourcc(*'mp4v')
print(h,w)
out = cv2.VideoWriter('Times different architecture/yolo_v4_output_2.mp4',fourcc, 30.0,(w,h))
for j in range (len(imgarr)):
    clear_output()
    out.write(imgarr[j])
    print('Frames Processed:',j,'/',frame_id)
out.release()
cv2.destroyAllWindows()