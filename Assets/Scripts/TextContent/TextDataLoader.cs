using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TextDataLoader : MonoBehaviour
{
    //[SerializeField] private DBTDataList dataList;
    [SerializeField]
    private List<DBTSOList> dbtsoLists;






    public string[] LoadTextData(string csvName)
    {
        string[] csvData;
        if (csvName == "snake2dTextData")
        {
            csvData = DataManager.instance.snake2dTextData.text.Split('\n').Skip(1).ToArray();
            return csvData;
        }

        else if (csvName == "categoryList")
        {
            csvData = DataManager.instance.categoryList.text.Trim().Split('\n');
            string[] cateColData = new string[csvData.Length - 1];//not counting the row of column name
            for (int i = 1; i < csvData.Length; i++)
            {
                string[] csvRowData = csvData[i].Split(';');
                cateColData[i - 1] = csvRowData[PlayerPrefs.GetInt("Language") + 1].Trim();//add 1 to skip the key col
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
