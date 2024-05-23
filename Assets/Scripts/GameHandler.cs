using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    //scene size
    [SerializeField]
    private int width;
    [SerializeField]
    private int height;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private List<GameObject> foodPrefabs;
    [SerializeField]
    private int numOfInitFoods = 6;

    [field: SerializeField]
    public float HP { get; private set; }
    private float MaxHP = 40;
    private float timer = 0;
    private int totalScore = 0;//food score

    private bool gameover = false;


    public List<CollectedFoodData> collectedFoodList { get; private set; } = new List<CollectedFoodData>();


    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1f;
        instance = this;
        for (int i = 0; i < numOfInitFoods; i++)
        {//generate some random foods when started
            SpawnFood();
        }

        HP = MaxHP;
        //UIManager.instance.UpdateScore(totalScore);

        gameover = false;
    }

    private void FixedUpdate()
    {
        HP -= Time.fixedDeltaTime;
        UIManager.instance.SetHPValue(HP / MaxHP);
        if (HP <= 0)
            EndGame("Your snake is too hungry");


        timer += Time.fixedDeltaTime;
        UIManager.instance.UpdateTimer(timer);
    }

    public void SpawnFood()
    {
        int foodIdx = UnityEngine.Random.Range(0, foodPrefabs.Count);

        Vector3Int randomPosition;
        do
        {
            randomPosition = new Vector3Int(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height), 0);
            //should not spawn on player's position
        } while (randomPosition == player.transform.position);


        Instantiate(foodPrefabs[foodIdx], randomPosition, Quaternion.identity);
    }

    public void EndGame(string message)
    {
        if (gameover)
            return;

        gameover = true;

        Debug.Log("Lose: " + message);
        AddRecordToDB(totalScore, timer);
        UIManager.instance.ShowGameOverPanel(message, totalScore, timer, collectedFoodList);
        Time.timeScale = 0f;
    }

    public void AddHP(float amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, MaxHP);
    }

    public void EatFood(string name, int score)
    {
        SpawnFood();
        AddHP(score / 5);
        totalScore += score;
        UIManager.instance.ShowEffectText(score);
        UIManager.instance.UpdateScore(totalScore);
        CollectedFoodData foodData = new CollectedFoodData(name, timer, score);
        collectedFoodList.Add(foodData);
        UIManager.instance.UpdateCollectedList(foodData);
    }


    public void AddRecordToDB(int score, float seconds)
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string timeStr = time.ToString(@"mm\:ss");
        int totalScore = ((int)timer + score);

        //realmController.AddRecord(playerName, score, timeStr, totalScore);
        DatabaseHandler.DB_instance.AddRecordToDB(playerName, score, timeStr, totalScore);
    }
}

public class CollectedFoodData
{
    public string foodName;
    public string time;
    public int score;

    public CollectedFoodData(string foodName, float seconds, int score)
    {
        this.foodName = foodName;
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        this.time = time.ToString(@"mm\:ss");
        this.score = score;
    }
}
