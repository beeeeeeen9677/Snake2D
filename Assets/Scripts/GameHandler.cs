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
    private int totalScore = 0;

    public List<CollectedFoodData> collectedFoodList { get; private set; } = new List<CollectedFoodData>();



    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        for (int i = 0; i < numOfInitFoods; i++)
        {//generate some random foods when started
            SpawnFood();
        }

        HP = MaxHP;
        //UIManager.instance.UpdateScore(totalScore);
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
        Debug.Log("Lose: " + message);

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
        CollectedFoodData foodData = new CollectedFoodData(name, timer);
        collectedFoodList.Add(foodData);
        UIManager.instance.UpdateCollectedList(foodData);
    }
}

public class CollectedFoodData
{
    public string foodName;
    public string time;

    public CollectedFoodData(string foodName, float seconds)
    {
        this.foodName = foodName;
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        this.time = time.ToString(@"mm\:ss");
    }
}
