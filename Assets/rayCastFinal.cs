using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class rayCastFinal : MonoBehaviour
{
    // Start is called before the first frame update
    public int count = 0;
    public int count2 = 0;
    public GameObject cam2;
    public Camera Camera2;
    public GameObject label;
    public int leng;
    public GameObject[] clone = new GameObject[40];
    public Vector3 offset = new Vector3(0.0f, 0.5f, 0.0f);

    void cast()
    {
        //GameObject[] clone = new GameObject[40];
        leng = getJsonData.leng;
        for (int i = 2; i < leng-1; i++)
        {
            float yCoord = ((getJsonData.coordinates[i+1, 0]) + (getJsonData.coordinates[i+1, 2])) / 2200.0f;
            float xCoord = ((getJsonData.coordinates[i+1, 1]) + (getJsonData.coordinates[i+1, 3])) / 2200.0f;
            Debug.Log(string.Format("Mouse Position: {0} Char Position: {1} Mouse Position: {2} Char Position: {3}", getJsonData.coordinates[i + 1, 0], getJsonData.coordinates[i + 1, 2], getJsonData.coordinates[i + 1, 1], getJsonData.coordinates[i + 1, 3]));
            Debug.Log(string.Format("{0} {1}",  xCoord, yCoord));
            Camera2.transform.position = CaptureImage2.position[count+1];
            Camera2.transform.eulerAngles = CaptureImage2.angles[count+1];
            Debug.Log(Camera2.fieldOfView);
            RaycastHit hit;
            Ray ray = Camera2.ViewportPointToRay(new Vector3(xCoord,1.0f - yCoord, 0));
            Debug.DrawRay(ray.origin, ray.direction*10.0f, Color.red, 500.0f, true);
            Physics.Raycast(ray, out hit);
            clone[i] = Instantiate(label, hit.point + /*(hit.normal) * 0.5f +*/ offset * 3, cam2.transform.rotation); 
            //clone[i].GetComponent<TextMeshPro>().text = getJsonData.labels[i];
        }
    }

    void text_change()
    {
        for (int i = 2; i < leng-1; i++)
        {
            clone[i].GetComponent<TextMeshPro>().text = getJsonData.labels[i-2];
        }
    }
    void Start()
    {
        label = GameObject.Find("Text (TMP)");
        cam2 = gameObject;
        //Component comp = cam2.GetComponent<Camera>();
        //comp.enabled = false;
        //comp.fieldOfView = 105.0f;
        //Camera2.fieldOfView = 105.0f;
        Camera2.enabled = false;

        /*Ray ray1 = Camera2.ViewportPointToRay(new Vector3(0.5f, 0.25f, 0));
            Debug.DrawRay(ray1.origin, ray1.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray2 = Camera2.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(ray2.origin, ray2.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray3 = Camera2.ViewportPointToRay(new Vector3(0.5f, 0.75f, 0));
            Debug.DrawRay(ray3.origin, ray3.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray4 = Camera2.ViewportPointToRay(new Vector3(0.5f, 1.0f, 0));
            Debug.DrawRay(ray4.origin, ray4.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray5 = Camera2.ViewportPointToRay(new Vector3(0.5f, 0.0f, 0));
            Debug.DrawRay(ray5.origin, ray5.direction * 10.0f, Color.blue, 500.0f, false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(getJsonData.done[count] == true && count2 == 0)
        {
            cast();
            count2++;
            /*Ray ray1 = Camera2.ViewportPointToRay(new Vector3(0.01f, 0.01f, 0));
            Debug.DrawRay(ray1.origin, ray1.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray2 = Camera2.ViewportPointToRay(new Vector3(0.99f, 0.99f, 0));
            Debug.DrawRay(ray2.origin, ray2.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray3 = Camera2.ViewportPointToRay(new Vector3(0.99f, 0.01f, 0));
            Debug.DrawRay(ray3.origin, ray3.direction * 10.0f, Color.blue, 500.0f, false);
            Ray ray4 = Camera2.ViewportPointToRay(new Vector3(0.01f, 0.99f, 0));
            Debug.DrawRay(ray4.origin, ray4.direction * 10.0f, Color.blue, 500.0f, false);*/
        }
        else
        {
            count--;
        }
        count++;
        count2++;
        if (count2 == 20)
        {
            text_change();
            count2 = 0;
        }
    }
}