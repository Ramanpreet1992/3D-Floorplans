using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upload : MonoBehaviour
{
    string path;
    public GameObject buttonPrefab;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    public GameObject Next;
    public GameObject Back;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateButtons()
    {

    }
    public void FetchData(string path)
    {
        StreamReader readData = new StreamReader(path);
        bool endOfFile = false;
        string a="0";
        string b = "0";
        var dataColumn=new List<string>();
        int count = 0;
        List<List<string>> list = new List<List<string>>();
        while (!endOfFile)
        {
            string data_string = readData.ReadLine();
            if (data_string == null)
            {
                endOfFile = true;
                break;
            }
            string[] data_values = data_string.Split(',');
            count = data_values.Length;
            for (int i = 0; i < data_values.Length; i++)
            {
                string data = data_values[i].ToString();
                dataColumn.Add(data);
                list.Add(dataColumn);
            }
        }
        Debug.Log(count);
        GameObject buttonParent=EventSystem.current.currentSelectedGameObject;
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
        buttonParent.SetActive(false);
        Next.SetActive(true);
        Back.SetActive(true);
        Debug.Log(buttonParent.transform.parent);
        for(int i=3;i<count;i++)
        { 

            a = list[i][i];
            Debug.Log(a);
            //GenerateItem(a);
            GameObject go = Instantiate(buttonPrefab);
            go.transform.parent = buttonParent.transform.parent;
            go.name = a;
            go.GetComponentInChildren<Text>().text = a;
            //go.Get<Text>().text = a.ToString();
           // go.transform.position = new Vector3(buttonParent.transform.position.x, buttonParent.transform.position.y + 30f, buttonParent.transform.position.z);
            var button = GetComponent<UnityEngine.UI.Button>();
           



        }
        scrollView.verticalNormalizedPosition = 1;

    }

    void GenerateItem(string itemNumber)
    {
        GameObject scrollItemObj = Instantiate(scrollItemPrefab);
        scrollItemObj.transform.SetParent(scrollContent.transform, false);
        scrollItemObj.transform.Find("num").gameObject.GetComponent<Text>().text = itemNumber;
        return;

    }

    public void ShowExplorer()
    {
        Debug.Log(this.transform.position);
       // path = EditorUtility.OpenFilePanel("Overwite with csv", "", "csv");
        //FetchData(path);
        
    }
}
