from pixellib.instance import instance_segmentation
import time
from PIL import Image
import json
import cv2
from os import path
import numpy as np

instance_seg = instance_segmentation()
instance_seg.load_model("C:\Local Disk D\Project\mask_rcnn_coco.h5")
for count2 in range(5):
    name = str(count2+1)+'.jpg'
    address = r'C:\Local Disk D\Project\Accuracy Images\\' + name
    cap=cv2.imread(address)
    frame_id = 0
    starting_time= time.time()
    imgarr = []
    times = []
    count = 0
#while True:
    start = time.time()
    #_,frame= cap.read() # for input video
    frame = cap
    #frame= cap.read() # for input web cam 
    frame_id+=1
    if frame is None or count == 30:
        print("Loop finished...BREAKING")
        break
    print(frame_id)
    count+=1
    #im = Image.open(frame)
    segmask, output = instance_seg.segmentImage(address, show_bboxes= True)
    #cv2.imwrite(place3, output)
    imgarr.append(output)
    end = time.time()
    times.append(end-start)
    print(end-start)
        
    np.savetxt('Times Different Architecture/'+str(count)+'mask_rcnn_2.csv', times, delimiter=',', fmt='%s')
        
    
    #fourcc = cv2.VideoWriter_fourcc(*'mp4v')
    name2 = str(count2+1)+'_res_maskrcnn.jpg'
    address2 = r'C:\Local Disk D\Project\Accuracy Images\\' + name2
    #out = cv2.VideoWriter('Times different architecture/mask_rcnn_output_2.mp4',fourcc, 30.0,(1280,720))
    cv2.imwrite(address2, output)
    '''for j in range (len(imgarr)):
        out.write(imgarr[j])
        print('Frames Processed:',j,'/',frame_id)
    out.release()
    cv2.destroyAllWindows()'''