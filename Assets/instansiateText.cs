using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class instansiateText : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject label;
    public GameObject clone;
    int count = 0;
    void Start()
    {

    }

    public void hugr()
    {
        label = GameObject.Find("Text (TMP)");
        //clone = Instantiate(label, new Vector3(2.0F, 0, 0), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        count++;
        if (count == 5)
        {
            hugr();
        }
        if(count == 10)
        {

            //clone.GetComponent<TextMeshPro>().text = "Itext";
        }
    }
}