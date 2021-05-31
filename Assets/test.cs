using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public int count = 0;
    public int count2 = 0;
    public GameObject Camera2;

    // Start is called before the first frame update
    void Start()
    {
        Camera2 = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        count2++;
        if (count2 == 5)
        {
            count2 = 0;
            //Camera2.GetComponent<Camera>().enabled = false;
            Camera2.transform.position = GameObject.Find("Main Camera").transform.position;
            Camera2.transform.rotation = GameObject.Find("Main Camera").transform.rotation;
        }

        Vector3 forward = Camera2.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Camera2.transform.position, forward, Color.red, 1.0f , false);

    }
}