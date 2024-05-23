using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private GameObject rankingDataPrefab;
    [SerializeField]
    private Transform rankingListPanel;
    public void ShowRanking()//order by total score
    {
        List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecord();

        ListAllRecords(records);
    }

    private void ListAllRecords(List<Snake2dRank> records)
    {
        ClearShowingList();
        Debug.Log(records.Count);

        RectTransform rt = rankingListPanel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (records.Count) * 100);


        if (records.Count == 0)
        {
            Debug.Log("No record exists or DB not connected");

            GameObject message = new GameObject("message", typeof(TextMeshProUGUI));
            TextMeshProUGUI msg = message.GetComponent<TextMeshProUGUI>();
            msg.text = "No record exists or connecting to DB\nPlease try to refresh later";
            msg.alignment = TextAlignmentOptions.Center;
            msg.color = Color.black;
            msg.fontSize = 38;
            message.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
            Instantiate(message, rankingListPanel);


            return;
        }
        foreach (Snake2dRank rec in records)
        {
            //Debug.Log(rec.Name + " " + rec.Score + " " + rec.Time + " " + rec.Total);
            GameObject data = Instantiate(rankingDataPrefab, rankingListPanel);
            data.GetComponent<RankingData>().SetData(rec.Name, (int)rec.Score, rec.Time, (int)rec.Total);
        }
    }

    public void ClearShowingList()
    {
        foreach (Transform record in rankingListPanel)
        {
            Destroy(record.gameObject);
        }
    }


    public void ShowRankingOrderByName()
    {
        List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecordOrderByName();

        ListAllRecords(records);
    }

    public void ShowRankingOrderByScore()
    {
        List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecordOrderByScore();

        ListAllRecords(records);
    }
    public void ShowRankingOrderByTime()
    {
        List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecordOrderByTime();

        ListAllRecords(records);
    }
}
