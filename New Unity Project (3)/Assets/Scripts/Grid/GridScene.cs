using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.XR.WSA;
using System.Linq;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;

public class GridScene : MonoBehaviour
{
    public GameObject parentPrefab;
    private BoundingBox bbox;
    private WorldAnchor anchor = null;
    public GameObject cylinderPrefab;
    public GameObject wallsPrefab;
    public int gridWidth = 11;
    public int gridHeight = 11;
    public string filePath;
    float cylWidth = 1;
    float cylHeight = 1;
    public float gap = 0.1f;
    public List<List<string>> listObjects;
    private List<string> xPoint ;
    private List<string> yPoint ;
    public List<string> VisualIntegration;
    public List<string> visualComplexity;
    public List<string> openness;
    private int xValues;
    private int yValues;
    public List<float> ranges;
    public List<List<float>> level =new List<List<float>>();
    public List<string> levelNames = new List<string>();
    public List<string> rangeNames;
    private int opennessValues;
    private int visualcomplexityValues;
    private int VisualIntegrationValues;
    public List<List<string>> visualValues;
    public RecolorGrid colorObject;
    public List<List<string>> colorObjects;
    public List<Transform> hexObjects;
    Vector3 startPos;
    Vector3 nextImage=new Vector3(0,0,0);
    float min;
    float max;
    List<string> sortedArray;
    List<int> sortedIndex;
    List<float> renderColor;



    public List<List<string>> ReadCSVfile()
    {
        //Enter the file
        filePath = Application.dataPath;
        filePath = filePath + "//Maps//GoldbergDetails.csv";
        xPoint = new List<string>();
        yPoint = new List<string>();
        openness = new List<string>();
        visualComplexity = new List<string>();
        VisualIntegration = new List<string>();
        visualValues = new List<List<string>>();
        StreamReader streamReader = new StreamReader(filePath);
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_string = streamReader.ReadLine();
            if (data_string == null)
            {
                endOfFile = true;
                break;
            }

            string[] data_values = data_string.Split(',');
            for (int i = 1; i < data_values.Length; i++)
            {
                //Fetching the x cordinate
                if (data_values[i].ToString() == "x")
                {
                    xValues = i;
                }

                //Fetching the y cordinate
                if (data_values[i].ToString() == "y")
                {
                    yValues = i;

                }
                
               //Fetching the Openness"
               if (data_values[i].ToString() == "Isovist Area")
               {
                  opennessValues = i;
                   
               }

               //Fetching the VisualIntegration"
               if (data_values[i].ToString() == "Visual Integration [HH]")
               {
                  VisualIntegrationValues = i;
               }

               //Fetching the Visual Complexity"
               if (data_values[i].ToString() == "Isovist Perimeter")
               {
                  visualcomplexityValues = i;
               }
                
            }
            if (xValues != 0 && yValues != 0 && VisualIntegrationValues != 0 && opennessValues != 0 && visualcomplexityValues != 0)
            {
                string x = data_values[xValues].ToString();
                string y = data_values[yValues].ToString();
                string open = data_values[opennessValues].ToString();
                string visual = data_values[visualcomplexityValues].ToString();
                string visualIntegration = data_values[VisualIntegrationValues].ToString();
                xPoint.Add(x);
                yPoint.Add(y);
                openness.Add(open);
                visualComplexity.Add(visual);
                VisualIntegration.Add(visualIntegration);
                


            }
        }
        listObjects = new List<List<string>>();
        listObjects.Add(xPoint);
        listObjects.Add(yPoint);
        listObjects.Add(openness);
        listObjects.Add(visualComplexity);
        listObjects.Add(VisualIntegration);
        level.Add(Range(openness));
        level.Add(Range(visualComplexity));
        level.Add(Range(VisualIntegration));
        return listObjects;


    }


    private void Start()
    {
        ReadCSVfile();
        AddGap();
        CalcStartPos();
        int j = 0;
        GameObject floorPlan = GameObject.FindGameObjectWithTag("FloorPlan");

        for (int i = 2; i < 3; i++)
        {
            GameObject parentObject = GameObject.Instantiate(parentPrefab);
            
           ///Transform parent = Instantiate(parentPrefab) as Transform;

            parentObject.name = listObjects[i][0];
           // bbox = parentObject.AddComponent<BoundingBox>();


            parentObject.transform.position = new Vector3(0, 0, 0);
            List<float[]> unsorted = new List<float[]>();
            List<float[]> sorted = new List<float[]>();
            renderColor = new List<float>();

            //used indexing to store the index for storing
            for (int k = 1; k < listObjects[i].Count; k++)
            {
                float[] temp = new float[2];
                temp[0] = float.Parse(listObjects[i][k]);
                temp[1] = k;
                unsorted.Add(temp);

            }

            sorted = MergeSort(unsorted);
           
            renderColor.Add(0);
            ColorGraph(sorted);


            CreateGrid(sorted, parentObject,1);
            j = j + 1;
           // CreateWalls(listObjects[i], parent);
            parentObject.transform.parent = floorPlan.transform;

            parentObject.transform.localPosition = new Vector3(-2f, 0f, 1.4f);
            parentObject.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
           // parentObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        }

    }
  
    
    public void AddGap()
    {
        cylWidth += cylWidth * gap;
        cylHeight += cylHeight * gap;

    }
    public void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
        {
            offset = cylWidth / 2;
        }

        float x = cylWidth * (gridWidth / 2) - offset;
        float z = cylHeight * 0.075f * (gridHeight / 2);

        startPos = new Vector3(0, 0, 0);


    }
    public Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float x = startPos.x + gridPos.x;
        float z = startPos.z - gridPos.y * 1f;

        return new Vector3(x, z, 0);
    }

    public Vector3 CalcWorldPosWall(Vector2 gridPos, int i)
    {
        float x = startPos.x + gridPos.x;
        float y = startPos.z - gridPos.y * 1f;
        //float z = 0 + i;
        float z = 0f;

        return new Vector3(x, y, z);
    }
    public List<Transform> CreateGrid(List<float[]> value, GameObject parent, int position)
    {
        List<float> index = new List<float>();      
        Vector3 hexposition;
        float nextPosition = position;
        int loop = 0;
        Vector3 textAnchor;
        for(int i=0;i<value.Count;i++)
        {
            index.Add(value[i][1]);
        }

        GameObject UItextGO = new GameObject("Text2");
        UItextGO.transform.parent=parent.transform;

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        TextMesh text = UItextGO.AddComponent<TextMesh>();

        // for (int i = 1; i < yPoint.Count; i++)
        for (int i = 1; i < yPoint.Count; i++)
        {
            int eastWall = 0;
            //Transform hex = Instantiate(cylinderPrefab) as Transform;
            GameObject hex = GameObject.Instantiate(cylinderPrefab);
            Vector2 gridPos = new Vector2((float.Parse(xPoint[i])/10)+nextImage.x+20*nextPosition ,(float.Parse(yPoint[i])/10));
            hex.transform.parent = parent.transform;
            textAnchor = CalcWorldPos(gridPos);

            if(i==1)
            {
                text.text = parent.name;
                trans.transform.position = new Vector3(textAnchor.x, textAnchor.y+5, 0);

                
            }

            hex.transform.position =  CalcWorldPos(gridPos);
            hexposition = hex.transform.position;
            hex.name = "Cylinder" + int.Parse(xPoint[i]) + "|" + int.Parse(yPoint[i]);

            Renderer rend = hex.GetComponentInChildren<Renderer>();
            int color = index.IndexOf(i);
            rend.material.color = Color.HSVToRGB(renderColor[color],1f,1f);
            hex.tag = "Grid";

            eastWall = int.Parse(xPoint[i]) + 1;
            if (!(xPoint.Contains((eastWall).ToString())) && loop==0)
            {
                nextImage = hexposition;
                loop = loop + 1;
            }
        }

        //Debug.Log(parent.GetComponent<Collider>().bounds.size);

        return hexObjects;
    }


    public List<float> Range(List<string> value)
    {

        List<List<string>> Range = new List<List<string>>();
        ranges = new List<float>() {0,0,0,0,0 };
        levelNames = new List<string>(new string[] { "Openess","Visual Complexity", "Visual Integration" });
        int levelNum = levelNames.Count;
        int len = value.Count;
        for(int i=0;i<levelNames.Count+1;i++)
        {
            ranges[i] = (len)/levelNum + (len%levelNum);
        }

       return ranges;
    }


    private static List<float[]> MergeSort(List<float[]> unsorted)
    {
        if (unsorted.Count <= 1)
            return unsorted;

        List<float[]> left = new List<float[]>();
        List<float[]> right = new List<float[]>();

        int middle = unsorted.Count / 2;
        for (int i = 0; i < middle; i++)  //Dividing the unsorted list
        {
            left.Add(unsorted[i]);
        }
        for (int i = middle; i < unsorted.Count; i++)
        {
            right.Add(unsorted[i]);
        }

        left = MergeSort(left);
        right = MergeSort(right);
        return Merge(left, right);
    }

    private static List<float[]> Merge(List<float[]> left, List<float[]> right)
    {
        List<float[]> result = new List<float[]>();

        while (left.Count > 0 || right.Count > 0)
        {
            if (left.Count > 0 && right.Count > 0)
            {
                if (left[0][0] <= right[0][0])  //Comparing First two elements to see which is smaller
                {
                    result.Add(left.First());
                    left.Remove(left.First());      //Rest of the list minus the first element
                }
                else
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            else if (left.Count > 0)
            {
                result.Add(left.First());
                left.Remove(left.First());
            }
            else if (right.Count > 0)
            {
                result.Add(right.First());

                right.Remove(right.First());
            }
        }
        
        return result;
    }

    public void ColorGraph(List<float[]> list)
    {

        int count = list.Count;
        int values = list.Count / 6;
        float Hue = 0;
        float number = 1 / 6f;
        int increment = values;
        for (int i = 0; i < list.Count; i++)
        {

            if (i == increment)
            {

                Hue = Hue + 1;
                increment = values + increment;

            }
            float valueHue = number * Hue;
            renderColor.Add(valueHue);


        }

           
    }
}
