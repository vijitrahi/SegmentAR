using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using Json.NET;

public class getJsonData : MonoBehaviour
{
    public int count = 0;
    public int count4;
    public static int leng;
    public static int[,] coordinates = new int[40, 4];
    public static string[] labels = new string[40];
    public static bool[] done = new bool[999];
    string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        count4 = 0;
        lines = File.ReadAllLines(@"D:\Vijit\Python Scripts\coco.txt");
        for (int i = 0; i<999; ++i)
        {
            done[i] = false;
        }
    }

    /*public static object DeserializeFromStream(FileStream stream)
    {
        var serializer = new JsonSerializer();

        using (var sr = new StreamReader(stream))
        using (var jsonTextReader = new JsonTextReader(sr))
        {
            return serializer.Deserialize(jsonTextReader);
        }
    }
    */


    void getData()
    {
        try
        {
            string name = @"D:\Vijit\Python Scripts\Output Data2\" + (count + 1).ToString() + ".json";
            string path = name;
            Debug.Log("hello");
            //string path = @"D:\Vijit\Python Scripts\Output Data\1.json";
            /*if(File.Exists(path))
            {
                System.Threading.Thread.Sleep(500);
            }*/
            FileStream dataImage;
            dataImage = File.Open(path, FileMode.Open);
            double size = dataImage.Length;
            if (true)
            {
                Debug.Log("hi");
                string total;
                using (var sr = new StreamReader(dataImage))
                {
                    total = sr.ReadToEnd();
                }
                dataImage.Close();
                if (total != "\"\"")
                {
                    string[] arr = total.Split('[');
                    //var serializer2 = DeserializeFromStream(dataImage);

                    //string[] point = new string[10];
                    int count3 = 0;
                    leng = arr.Length;
                    foreach (string points in arr)
                    {
                        count3++;
                        string[] point2 = points.Split(',');
                        /*if (count > 1)
                        {
                            point[count, 0] = int.Parse(points.Substring(0, 3));
                            point[count, 1] = int.Parse(points.Substring(4, 3));
                            point[count, 2] = int.Parse(points.Substring(9, 3));
                            point[count, 3] = int.Parse(points.Substring(13, 3));
                            count++;
                        }
                        else
                        {
                            count++;
                        }
                        */
                        //Debug.Log(point2.Length);
                        if (point2.Length == 5 && count3 != leng)
                        {
                            int count2 = 0;
                            foreach (string point in point2)
                            {
                                if (count2 < 4)
                                {
                                    string pointk = point.Replace(",", "");
                                    pointk = pointk.Replace("]", "");
                                    pointk = pointk.Replace("'", "");
                                    pointk = pointk.Replace(" ", "");
                                    pointk = pointk.Replace("\"", "");
                                    pointk = pointk.Replace(",", "");
                                    coordinates[count3, count2] = int.Parse(pointk);
                                    //Debug.Log(coordinates[count3, count2]);
                                    count2++;
                                }
                            }
                            //Debug.Log(coordinates[count3, 2]);
                        }
                        if (count3 == leng)
                        {
                            int count2 = 0;
                            foreach (var point in point2)
                            {
                                string pointk = point.Replace(",", "");
                                pointk = pointk.Replace("]", "");
                                pointk = pointk.Replace("'", "");
                                pointk = pointk.Replace(" ", "");
                                pointk = pointk.Replace("\"", "");
                                pointk = pointk.Replace(",", "");
                                labels[count2] = pointk;
                                labels[count2] = lines[int.Parse(labels[count2]) - 1];
                                //Debug.Log(labels[count2]);
                                count2++;
                            }
                        }
                    }
                    //Debug.Log(labels.Length);
                    done[count] = true;
                }
            }
        }
        catch (Exception FileNotFoundException)
        {
            Debug.Log(FileNotFoundException);
            count--;
        }
        count++;
    }





    // Update is called once per frame
    void Update()
    {
        string name = @"D:\Vijit\Python Scripts\Output Data\" + (count + 1).ToString() + ".json";
        string path = name;

        if (File.Exists(path) && count4 >= 0)
        {
            count4++;
        }
        if(count4 == 20)
        {
            getData();
            count4 = 0;
        }
    }
}