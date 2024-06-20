using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseHandler : MonoBehaviour
{
    public static DatabaseHandler DB_instance;

    private string[] records;
    private string insertDataUrl = "https://beeeeeeenhihi.000webhostapp.com/insertData.php"; //api for inserting data
    //private string getDataUrl = "https://beeeeeeenhihi.000webhostapp.com/ranking.php"    //api for getting data


    public Action<List<DataManager>> OnDataLoaded;


    private void Awake()
    {
        if (DB_instance == null)
            DB_instance = this;
        else if (DB_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }





    public void GetAllRecords(string orderBy) //load all records
    {
        StartCoroutine(LoadRecord(orderBy));
    }


    IEnumerator LoadRecord(string orderBy)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://beeeeeeenhihi.000webhostapp.com/ranking.php" + "?orderBy=" + orderBy);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string recordDataStr = www.downloadHandler.text;
            //Debug.Log(recordDataStr);
            records = recordDataStr.Split(';');
            //Debug.Log(records.Length);
            /*
            Debug.Log(GetDataValue(records[0], "Name"));
            Debug.Log(GetDataValue(records[0], "Score"));
            Debug.Log(GetDataValue(records[0], "Time"));
            Debug.Log(GetDataValue(records[0], "TotalScore"));
            */


            System.Collections.Generic.List<DataManager> rankingData
                = new System.Collections.Generic.List<DataManager>();
            foreach (string record in records)
            {
                if (record == "")
                    break; //last splitted string

                DataManager data = new DataManager(
                    GetDataValue(record, "Name"),
                    int.Parse(GetDataValue(record, "Score")),
                    GetDataValue(record, "Time"),
                    int.Parse(GetDataValue(record, "TotalScore"))
                    );


                //Debug.Log(data.Total);

                rankingData.Add(data);
            }

            if (orderBy == "Name") //in asc order
                rankingData.Reverse();


            OnDataLoaded?.Invoke(rankingData);
        }
    }

    string GetDataValue(string data, string col)
    {
        string value = data.Substring(data.IndexOf(col) + col.Length);
        value = value.Remove(value.IndexOf("|"));
        return value;
    }



    //upload new record
    public void AddRecordToDB(string playerName, int score, string timeStr, int totalScore)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerNamePost", playerName);
        form.AddField("scorePost", score);
        form.AddField("timePost", timeStr);
        form.AddField("totalScorePost", totalScore);

        StartCoroutine(Upload(form));
    }


    IEnumerator Upload(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(insertDataUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Record upload complete!");
            }
        }
    }




}
