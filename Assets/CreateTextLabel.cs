using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreateTextLabel : MonoBehaviour
{

    public GameObject texr;
    // Start is called before the first frame update
    void Start()
    {
        texr = gameObject;
        texr.GetComponent<TextMeshPro>().text = "Whatever";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
