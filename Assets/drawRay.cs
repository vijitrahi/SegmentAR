using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class drawRay : MonoBehaviour
{
    public float length = 1;
    public int count = 0;
    public int count2 = 0;
    //public GameObject cam2;
    public Camera MainCamera;
    //public Camera Camera2;
    public GameObject label;
    public GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        //Camera2 = GameObject.Find("Camera2");
        //MainCamera = GameObject.Find("Main Camera");
      // Camera2.enabled = false;
        MainCamera.enabled = true;
        label = GameObject.Find("Text (TMP)");
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Camera2").transform.position = new Vector3(1.0F + count/100.0F, 2.0F, 3.0F);
        //GameObject.Find("Camera2").transform.rotation = Quaternion.Euler(30.0f, -90.0f, 91.0f);
        //cam2 = gameObject;
        //Vector3 forward = cam2.transform.TransformDirection(Vector3.forward) * length;
        //Debug.DrawRay(cam2.transform.position, forward, Color.red, 1.0f , false);
        //length = length + 0.01f;
        count++;
        //RaycastHit hit;
        if(count >200 && count2 == 2)
        {
            clone.GetComponent<TextMeshPro>().text = "Itext";
            count2 = 0;
        }
        if (count == 200)
        {
            //GameObject.Find("Camera2").transform.position = CaptureImage2.position[1];
            //GameObject.Find("Camera2").transform.rotation = CaptureImage2.angles[1];
            //Vector3 forward2 = cam2.transform.TransformDirection(Vector3.forward);
            //Debug.Log(cam2.transform.position);
            //Debug.Log(cam2.transform.TransformDirection(Vector3.forward).ToString("F4"));
            //Physics.Raycast(transform.position, forward2, out hit);
            //Ray ray = Camera2.ViewportPointToRay(new Vector3(0.1F, 0.1F, 0));
           // Physics.Raycast(ray, out hit);
            //clone = Instantiate(label, hit.point + hit.normal, cam2.transform.rotation);
            count2 = 0;
        }
        count2++;
    }
}