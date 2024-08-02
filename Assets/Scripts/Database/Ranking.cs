using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour // ranking list
{
    [SerializeField]
    private GameObject rankingDataPrefab;
    [SerializeField]
    private Transform rankingListPanel;

    [SerializeField]
    private GameObject[] labelBtnList;


    public void ShowRanking()//order by total score
    {
        //List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecord();
        //ListAllRecords(records);
        ClearShowingList();
        IntantiateMessage("Loading records...");


        DatabaseHandler.DB_instance.GetAllRecords("TotalScore");
        ShowBaseImage(3);
    }


    private void ListAllRecords(List<Snake2dPlayerRankingData> records)//show all records
    {
        ClearShowingList();
        //Debug.Log(records.Count);

        RectTransform rt = rankingListPanel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (records.Count) * rankingDataPrefab.GetComponent<RectTransform>().rect.height);


        if (records.Count == 0)
        {
            Debug.Log("No record exists or DB not connected");
            IntantiateMessage("No record exists or connecting to DB\nPlease try to refresh later");

            return;
        }
        foreach (Snake2dPlayerRankingData rec in records)
        {
            //Debug.Log(rec.Name + " " + rec.Score + " " + rec.Time + " " + rec.Total);
            GameObject data = Instantiate(rankingDataPrefab, rankingListPanel);
            data.GetComponent<RankingData>().SetData(rec.Name, (int)rec.Score, rec.Time, (int)rec.Total);

        }
    }

    private void IntantiateMessage(string textMsg)
    {
        GameObject message = new GameObject("message", typeof(TextMeshProUGUI));
        //message.AddComponent<CanvasRenderer>();
        TextMeshProUGUI msg = message.GetComponent<TextMeshProUGUI>();
        msg.text = textMsg;
        msg.alignment = TextAlignmentOptions.Center;
        msg.color = Color.black;
        msg.fontSize = 38;
        message.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
        Instantiate(message, rankingListPanel);
        //StartCoroutine(CheckMsg(ptmessage));
    }

    /*
    IEnumerator CheckMsg(GameObject msg)
    {
        while (true)
        {
            Debug.Log(msg.transform.parent.gameObject.name);
            yield return null;
        }
    }
    */


    public void ClearShowingList()
    {
        foreach (Transform record in rankingListPanel)
        {
            Destroy(record.gameObject);
        }
    }



    public void ShowRankingOrderByName()
    {
        //List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecordOrderByName();
        //ListAllRecords(records);

        DatabaseHandler.DB_instance.GetAllRecords("Name");
        ShowBaseImage(0);
    }

    public void ShowRankingOrderByScore()
    {
        //List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecordOrderByScore();
        //ListAllRecords(records);

        DatabaseHandler.DB_instance.GetAllRecords("Score");
        ShowBaseImage(1);
    }
    public void ShowRankingOrderByTime()
    {
        //List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecordOrderByTime();
        //ListAllRecords(records);

        DatabaseHandler.DB_instance.GetAllRecords("Time");
        ShowBaseImage(2);
    }



    private void OnEnable()
    {
        DatabaseHandler.DB_instance.OnDataLoaded += ListAllRecords;
    }
    private void OnDestroy()
    {
        DatabaseHandler.DB_instance.OnDataLoaded -= ListAllRecords;
    }


    private void ShowBaseImage(int idx)//button feedback
    {
        foreach (GameObject btn in labelBtnList)
        {   //reset
            btn.SetActive(false);
        }
        labelBtnList[idx].SetActive(true);
    }

}
