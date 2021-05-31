using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callibrateCamera : MonoBehaviour
{
    public int count = 0;
    public int count2 = 0;
    public bool what = false;
    public GameObject cam2;
    public Camera Camera2;
    public GameObject label;
    public int leng;
    public GameObject[] clone = new GameObject[40];
    public Vector3[] coord = new Vector3[100];
    public Vector3 offset = new Vector3(0.0f, 0.0f, 0.5f);
    // Start is called before the first frame update
    void Start()
    {
        coord[0] = new Vector3(0.05f,0.05f,0);
        coord[1] = new Vector3(0.95f, 0.95f, 0);
        coord[2] = new Vector3(0.05f, 0.95f, 0);
        coord[3] = new Vector3(0.95f, 0.05f, 0);

        label = GameObject.Find("Text (TMP)");
        cam2 = gameObject;
    }

    // Update is called once per frame

    void cast()
    {
        //GameObject[] clone = new GameObject[40];
        /*for (int i = 0; i < 4; i++)
        {
            Camera2.transform.position = CaptureImage2.position[count + 1];
            Camera2.transform.eulerAngles = CaptureImage2.angles[count + 1];
            RaycastHit hit;
            Ray ray = Camera2.ViewportPointToRay(coord[i]);
            Physics.Raycast(ray, out hit);
            clone[i] = Instantiate(label, hit.point + (hit.normal)*0.1f + offset, cam2.transform.rotation);
            //clone[i].GetComponent<TextMeshPro>().text = getJsonData.labels[i];
        }*/

        Camera2.transform.position = CaptureImage2.position[count + 1];
        Camera2.transform.eulerAngles = CaptureImage2.angles[count + 1];
        for ( int i = 0; i < 10; i++)
        {
            for ( int j =0; j < 10; j++)
            {
                RaycastHit hit;
                coord[i + 10 * j] = new Vector3(0.00f + i / 10.0f, 0.00f + j / 10.0f, 0);
                Ray ray = Camera2.ViewportPointToRay(coord[i + 10 * j]);
                Physics.Raycast(ray, out hit);
                clone[i + 10*j] = Instantiate(label, hit.point /*+ (hit.normal) * 0.1f + offset*/, cam2.transform.rotation);
                Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.blue, 500.0f, false);
            }
        }
    }

    void Update()
    {
        if (what == true)
        {
            count2++;
            if (count2 == 50)
            {
                cast();
            }
        }
    }


   public void called()
    {

        what = true;

    }


}
