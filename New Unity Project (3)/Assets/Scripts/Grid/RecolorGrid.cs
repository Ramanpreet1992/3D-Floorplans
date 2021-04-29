using UnityEngine;
using System.Collections.Generic;
using System;


public class RecolorGrid : MonoBehaviour
{
    public Transform[] colorObject;
    public List<List<string>> list;
    public GridScene grids = new GridScene();
   
    public void reColor(int[] selection, GameObject[] objectColor)
    {
        list=grids.ReadCSVfile();
        int groupName = selection[1];
        for(int i=0; i<objectColor.Length;i++)
        {
            if (i == 0)
            {
                int j = 1;
                if (((int)Math.Round(float.Parse(list[groupName][j]))) > 20 && ((int)Math.Round(float.Parse(list[groupName][j]))) < 500)
                {
                    Renderer rend = objectColor[i].GetComponentInChildren<Renderer>();
                    rend.material.color = Color.blue;
                }
                if (((int)Math.Round(float.Parse(list[groupName][j]))) < 500 && ((int)Math.Round(float.Parse(list[groupName][j]))) < 1000)
                {
                    Renderer rend = objectColor[i].GetComponentInChildren<Renderer>();
                    rend.material.color = Color.green;
                }
                if (((int)Math.Round(float.Parse(list[groupName][j]))) < 1000 && ((int)Math.Round(float.Parse(list[groupName][j]))) < 2000)
                {
                    Renderer rend = objectColor[i].GetComponentInChildren<Renderer>();
                    rend.material.color = Color.red;
                }
            }
            else
            {
                if (((int)Math.Round(float.Parse(list[groupName][i]))) > 20 && ((int)Math.Round(float.Parse(list[groupName][i]))) < 500)
                {
                    Renderer rend = objectColor[i].GetComponentInChildren<Renderer>();
                    rend.material.color = Color.black;
                }
                if (((int)Math.Round(float.Parse(list[groupName][i]))) < 500 && ((int)Math.Round(float.Parse(list[groupName][i]))) < 1000)
                {
                    Renderer rend = objectColor[i].GetComponentInChildren<Renderer>();
                    rend.material.color = Color.yellow;
                }
                if (((int)Math.Round(float.Parse(list[groupName][i]))) < 1000 && ((int)Math.Round(float.Parse(list[groupName][i]))) < 2000)
                {
                    Renderer rend = objectColor[i].GetComponentInChildren<Renderer>();
                    rend.material.color = Color.red;
                }
            }
        }
    }


    // Start is called before the first frame update   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
