using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPlanClicker : MonoBehaviour
{
    GameObject go;
    Transform parent;
    Color colorTile;
    public Outline outline;

    public Action<GameObject> PointerDownOnBar;
    public Action<GameObject> PointerUpOnBar;
    public Action<GameObject> PointerEnterOnBar;
    public Action<GameObject> PointerExitOnBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnMouseDown()
    {
        
        go = this.gameObject;
        go.transform.localScale = new Vector3(1,10,1);
        colorTile = go.GetComponent<Renderer>().material.GetColor("_Color");
        go.GetComponent<Renderer>().material.color = Color.yellow;
        
        
    }
    public void OnMouseOver()
    {
       
        go = this.gameObject;
        go.transform.localScale = new Vector3(1, 10, 1);
        colorTile = go.GetComponent<Renderer>().material.GetColor("_Color");
        go.GetComponent<Renderer>().material.color = Color.yellow;
      

    }
    public void OnMouseExit()
    {
        go.GetComponent<Renderer>().material.color = colorTile;
        go.transform.localScale = new Vector3(1, 1, 1);
    }
    public void OnMouseDrag()
    {
        parent = this.transform.parent;
        float distance_to_screen = Camera.main.WorldToScreenPoint(parent.transform.position).z;
        parent.transform.position = Camera.main.WorldToScreenPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    }

}