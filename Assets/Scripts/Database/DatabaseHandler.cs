using System.Collections.Generic;
using UnityEngine;

public class DatabaseHandler : MonoBehaviour
{
    public static DatabaseHandler DB_instance;
    private RealmController realmController;


    private void Awake()
    {
        if (DB_instance == null)
        {
            DB_instance = this;
            realmController = new RealmController();

        }
        else if (DB_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }



    public void AddRecordToDB(string playerName, int score, string timeStr, int totalScore)
    {
        realmController.AddRecord(playerName, score, timeStr, totalScore);
    }

    public List<Snake2dRank> GetAllRecord()
    {
        return realmController.GetAllRecord();
    }
    public List<Snake2dRank> GetAllRecordOrderByName()
    {
        return realmController.GetAllRecordOrderByName();
    }

    public List<Snake2dRank> GetAllRecordOrderByScore()
    {
        return realmController.GetAllRecordOrderByScore();
    }
    public List<Snake2dRank> GetAllRecordOrderByTime()
    {
        return realmController.GetAllRecordOrderByTime();
    }

}
