from pixellib.instance import instance_segmentation
import cv2

instance_seg = instance_segmentation(infer_speed = "rapid")
instance_seg.load_model("D:\Vijit\Python Scripts\mask_rcnn_coco.h5")

segmask, output = instance_seg.segmentImage(r"D:\Vijit\Python Scripts\14.jpg", show_bboxes= True)
cv2.imwrite(r"D:\Vijit\Python Scripts\36.jpg", output)
print(output.shape)
