using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public Quaternion orientation;
    public Vector3 angles;
    public bool moveForward = false;
    public bool moveRight = false;
    public bool moveLeft = false;
    public bool moveReverse = false;
    public float speedForward = 0.02f;
    public float m_Thrust = 0.0020f;
    public int count = 0;
    Vector3 Vec;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    
    public void stop()
    {
        moveRight = false;
        moveLeft = false;
        moveForward = false;
        moveReverse = false;
    }

    public void fast()
    {
        speedForward += 0.005f;
    }
    
    public void slow()
    {
        speedForward -= 0.005f;
    }
    
    public void changeMoveForward()
    {
        moveForward = !moveForward;
        if (moveRight) 
        { 
            moveRight = false;
        }
        if (moveLeft) 
        { 
            moveLeft = false; 
        }
        if (moveReverse)
        { 
            moveReverse = false; 
        }
    }
    public void changeMoveRight()
    {
        moveRight = !moveRight;
        if (moveLeft)
        { 
            moveLeft = false;
        }
        if (moveForward)
        {
            moveForward = false;
        }
        if (moveReverse)
        { 
            moveReverse = false;
        }
    }
    public void changeMoveLeft()
    {
        moveLeft = !moveLeft;
        if (moveRight)
        { 
            moveRight = false;
        }
        if (moveForward)
        {
            moveForward = false;
        }
        if (moveReverse) 
        { 
            moveReverse = false;
        }
    }
    public void changeMoveReverse()
    {
        moveReverse = !moveReverse;
        if (moveRight) 
        { 
            moveRight = false;
        }
        if (moveLeft) 
        { 
            moveLeft = false; 
        }
        if (moveForward)
        { 
            moveForward = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name != "city7_floor")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
            if (moveRight || moveLeft || moveForward)
            {
                moveRight = false;
                moveLeft = false;
                moveForward = false;
                moveReverse = true;
            }
            else
            {
                moveReverse = false;
                moveRight = true;
                /*System.Random random = new System.Random();
                int num = random.Next();
                if (num % 3 == 0)
                {
                    moveForward = true;
                }
                if (num % 3 == 1)
                {
                    moveLeft = true;
                }
                if (num % 3 == 2)
                {
                    moveRight = true;
                }*/
            }
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        //if (collision.gameObject.tag == "MyGameObjectTag")
        //{
            //If the GameObject has the same tag as specified, output this message in the console
         //   Debug.Log("Do something else here");
       // }
    }





    // Update is called once per frame
    void Update()
    {
        orientation = m_Rigidbody.rotation;
        angles = orientation.eulerAngles;
        //Console.WriteLine(angles);
        if(((angles[0] > 30 && angles[0] < 330) || (angles[2] > 30 && angles[2] < 330)) && count > 10)
        {
            count = 0;
            if (moveRight || moveLeft || moveForward) 
            {
                moveRight = false;
                moveLeft = false;
                moveForward = false;
                moveReverse = true; 
            }
            else
            { 
                moveReverse = false;
                System.Random random = new System.Random();
                int num = random.Next();
                if (num % 3 == 0)
                {
                    moveForward = true;
                }
                if (num % 3 == 1)
                {
                    moveLeft = true;
                }
                if (num % 3 == 2)
                {
                    moveRight = true;
                }
            }
        }
        count++;
        if (false)
        {
            //m_Rigidbody.AddForce(transform.right * m_Thrust);
        }
        if (moveForward)
        {
            //m_Rigidbody.AddForce(-transform.right * m_Thrust);
            transform.Translate(0.0f,0f, -speedForward);
        }
        if (moveRight)
        {
            transform.Translate(0.00f, 0f, -speedForward);
            transform.Rotate(0.00f, 0.3f, -0.00f);
        }
        if (moveLeft)
        {
            transform.Translate(-0.000f, 0f, -speedForward);
            transform.Rotate(0.00f, -0.3f, -0.00f);
        }
        if (moveReverse)
        {
            transform.Translate(-0.00f, 0f, speedForward);
        }
    }
}
