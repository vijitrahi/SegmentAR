using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class attach_to_rover : MonoBehaviour
{

    /*
    Microsoft.MixedReality.Toolkit.Utilities.Solvers myScript;
    void Start()
    {
        myScript = gameObject.GetComponent<SolverHandler>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            myScript.enabled = !myScript.enabled;
        }
    }
    */

     public GameObject TargetObject;

     public void detach()
     {
         TargetObject = gameObject;
         Component comp = TargetObject.GetComponent<SolverHandler>();
         MonoBehaviour bhvr = (MonoBehaviour)comp;
         bhvr.enabled = false;
     }

    public void attach()
    {
        TargetObject = gameObject;
        Component comp = TargetObject.GetComponent<SolverHandler>();
        MonoBehaviour bhvr = (MonoBehaviour)comp;
        bhvr.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
     {
         //TargetObject = gameObject;
     }

     // Update is called once per frame
     void Update()
     {
        // Component comp = TargetObject.GetComponent("SolverHandler");
        // MonoBehaviour bhvr = (MonoBehaviour)comp;
     }
    
}