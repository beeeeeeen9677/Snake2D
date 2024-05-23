using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public void ShowRanking()
    {
        List<Snake2dRank> records = DatabaseHandler.DB_instance.GetAllRecord();

        foreach (Snake2dRank rec in records)
        {
            Debug.Log(rec.Name + " " + rec.Score + " " + rec.Time + " " + rec.Total);
        }
    }
}
