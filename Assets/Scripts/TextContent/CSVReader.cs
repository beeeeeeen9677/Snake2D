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
    private string[] moodIdx;
    [SerializeField]
    private TextAsset categoryListCsvFile;


    /*
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
    */


    public string[] ReadCSV(string csvName)
    {
        string[] csvData;
        if (csvName == "snake2dTextData")
        {
            csvData = textAssetData.text.Split('\n');
            return csvData;
        }

        else if (csvName == "categoryList")
        {
            csvData = categoryListCsvFile.text.Trim().Split('\n');
            string[] cateColData = new string[csvData.Length - 1];//not counting the row of column name
            for (int i = 1; i < csvData.Length; i++)
            {
                string[] csvRowData = csvData[i].Split(';');
                cateColData[i - 1] = csvRowData[PlayerPrefs.GetInt("Language")].Trim();
            }


            return cateColData;
        }

        else
        {
            Debug.Log("No such file");
            return null;
        }


    }

}

[Serializable]
public class DBTData
{
    public string text;
    public string category;
}
