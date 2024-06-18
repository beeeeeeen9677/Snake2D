using System;
using System.Collections.Generic;
using UnityEngine;
public class CSVReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset textAssetData;
    //[SerializeField] private DBTDataList dataList;
    [SerializeField]
    private List<DBTSOList> dbtsoLists;
    //[SerializeField]
    private string[] moodIdx;


    /*
    private void Awake()
    {
        //GetTextData();
    }
    */


    // Start is called before the first frame update
    public void GetTextData()  //call by GameHandler
    {
        //dataList = new DBTDataList();
        //dataList.dbtDataList = new List<DBTSO>();
        //dbtsoLists = new List<DBTSOList>();  //init in inspector

        moodIdx = new string[dbtsoLists.Count];
        for (int i = 0; i < dbtsoLists.Count; i++)
        {
            moodIdx[i] = dbtsoLists[i].mood;
        }

        //clear the list of all DBTSOList
        foreach (DBTSOList dbtlist in dbtsoLists)
        {
            dbtlist.dbtList.Clear();
        }

        ReadCSV();
    }


    private void ReadCSV()
    {
        int numOfCol = 3;

        string[] csvData = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        //int numOfRow = csvData.Length / numOfCol;//number of row

        //Debug.Log(csvData[570]);

        for (int i = 0; i < csvData.Length; i += numOfCol)
        {
            if (csvData[i].Trim() == "")
                break;
            //Debug.Log(i + ": " + csvData[i]);
            //create new DBT data and add into list

            DBTData data = new DBTData();

            //DBTData data = new DBTData();
            data.text = csvData[i].Trim();
            data.category = csvData[i + 1].Trim();
            //data.mood = csvData[i + 2];

            int listIndex = Array.IndexOf(moodIdx, csvData[i + 2].Trim());//find corresponding DBTSO List by mood
            //Debug.Log(listIndex);
            dbtsoLists[listIndex].dbtList.Add(data);
        }
    }

}

[Serializable]
public class DBTData
{
    public string text;
    public string category;
}
