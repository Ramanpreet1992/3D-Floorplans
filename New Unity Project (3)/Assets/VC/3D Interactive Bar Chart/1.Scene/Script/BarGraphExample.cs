using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using BarGraph.VittorCloud;

public class BarGraphExample : MonoBehaviour
{
    public List<BarGraphDataSet> exampleDataSet; // public data set for inserting data into the bar graph
    BarGraphGenerator barGraphGenerator;
    GridScene barData = new GridScene();
    public int listOfBars = 0;
    public List<List<string>> listData;
    public String[] data= { "Very Low", "Low", "Medium","High","Very High" };
    public List<Color> barColor = new List<Color>{ Color.magenta, Color.blue, Color.green,Color.yellow,Color.red };
    public List<Transform> colorObjects;


    public void Start()
    {
        barGraphGenerator = GetComponent<BarGraphGenerator>();
        listData = barData.ReadCSVfile();
        GameObject[] color = GameObject.FindGameObjectsWithTag("Grid");

        exampleDataSet = new List<BarGraphDataSet>() ;
        for (int i=0;i<data.Length;i++)
        {

            BarGraphDataSet bar = new BarGraphDataSet();
            bar.GroupName = data[i];
            List<XYBarValues> listXYData = new List<XYBarValues>();
            bar.barColor = barColor[i];
            for (int j = 0; j < barData.levelNames.Count; j++)
            {
                
                XYBarValues xyValues = new XYBarValues();
                xyValues.XValue = barData.levelNames[j];
                xyValues.YValue = barData.level[i][j];
                listXYData.Add(xyValues);


            }
            bar.ListOfBars = new List<XYBarValues>(listXYData);
            exampleDataSet.Add(bar);

        }



       
        
        //if the exampleDataSet list is empty then return.
        if (exampleDataSet.Count == 0)
        {

            Debug.LogError("ExampleDataSet is Empty!");
            return;
        }
        barGraphGenerator.GeneratBarGraph(exampleDataSet);

    }
    //call when the graph starting animation completed,  for updating the data on run time
    public void StartUpdatingGraph()
    {

       
        StartCoroutine(CreateDataSet());
    }



    IEnumerator CreateDataSet()
    {
        //  yield return new WaitForSeconds(3.0f);
        while (true)
        {

            GenerateRandomData();

            yield return new WaitForSeconds(2.0f);

        }

    }



    //Generates the random data for the created bars
    void GenerateRandomData()
    {
        
        int dataSetIndex = UnityEngine.Random.Range(0, exampleDataSet.Count);
        int xyValueIndex = UnityEngine.Random.Range(0, exampleDataSet[dataSetIndex].ListOfBars.Count);
        exampleDataSet[dataSetIndex].ListOfBars[xyValueIndex].YValue = UnityEngine.Random.Range(barGraphGenerator.yMinValue, barGraphGenerator.yMaxValue);
        barGraphGenerator.AddNewDataSet(dataSetIndex, xyValueIndex, exampleDataSet[dataSetIndex].ListOfBars[xyValueIndex].YValue);
    }
}



