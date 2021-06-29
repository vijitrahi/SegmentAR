from pixellib.instance import instance_segmentation
import time
from PIL import Image
import json
import cv2
from os import path

instance_seg = instance_segmentation()
instance_seg.load_model("C:\Local Disk D\Project\mask_rcnn_coco.h5")

count = 0
file1 = open('coco.txt', 'r')
Lines = file1.readlines()
file1.close()
while(1):
    
    name = str(count+1) + '.jpg'
    name2 = str(count+1) + '.json'
    name3 = str(count+1) + '_output.jpg'
    place = 'Images/' + name
    place2 = 'Output Data/' + name2
    place3 = 'Images/' + name3
    if(path.exists(place)):
        time.sleep(0.5)
        try:
            im = Image.open(place)
            segmask, output = instance_seg.segmentImage(place, show_bboxes= True)
            cv2.imwrite(place3, output)
            del segmask['scores']
            del segmask['masks']
            segmask['rois'] = list(segmask['rois'])
            segmask['class_ids'] = list(segmask['class_ids'])
            segmask = str(segmask)
            if(len(segmask) > 31):
                segmask = json.dumps(segmask)
                #a_file = open(place2, 'w')
                print(output.shape)
                #json.dump(segmask, a_file)
                #a_file.close()
                #ids2 = a_file.read()
                #a_file.close()
                ids3 = json.loads(segmask)
                ids4 = ids3[9 : len(ids3)-1]
                ids5 = ids4.split('array')
                ids6 = [1 for i in range(len(ids5))]
                for i in range(len(ids5)-1):
                    if(i != (len(ids5)-2)):
                        ids6[i] = ids5[i+1][1:len(ids5[i+1])-3]
                    else:
                        ids7 = ids5[len(ids5)-1].split('class_ids')
                ids6[len(ids5)-2] = ids7[0][1:len(ids7[0])-5]
                ids6[len(ids5)-1] = ids7[1][3:len(ids7[1])]
                ids6 = str(ids6)
            else:
                ids6 = ''
            a_file = open(place2, 'w')
            json.dump(ids6, a_file)
            a_file.close()
        except OSError:
            count-=1
    else:
        count-=1
    count+=1
    time.sleep(0.5)