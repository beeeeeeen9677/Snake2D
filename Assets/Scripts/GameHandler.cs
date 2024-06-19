using System;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject foodPrefabs;

    [SerializeField]
    private List<DBTSOList> scenarioList;
    private DBTSOList dbtsoList;
    [SerializeField]
    private int numOfInitFoods = 6;

    [field: SerializeField]
    public float HP { get; private set; }
    private float MaxHP = 80;
    private float timer = 0;
    private int totalScore = 0;//food score

    private bool gameover = false;


    public List<CollectedFoodData> collectedFoodList { get; private set; } = new List<CollectedFoodData>();

    private int dbtListLength;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private GameObject stonePrefab;

    private CSVReader csvReader;
    [SerializeField]
    private string[] moodIdx;





    // Start is called before the first frame update
    void Awake()
    {
        csvReader = GetComponent<CSVReader>();
        //csvReader.GetTextData();

        InitDBTListData();

        Food.categoryList = csvReader.ReadCSV("categoryList");
        for (int i = 0; i < Food.categoryList.Length; i++)
        {
            Food.categoryList[i] = Food.categoryList[i].Trim();
        }


        //choose scenario, random if not selected by player in main menu
        int mood = PlayerPrefs.GetInt("Mood");
        if (mood == 0)//default
        {
            long timestamp = (long)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var random = new System.Random((int)timestamp);
            // Generate a random number between 0 and dbtListLength
            int randomIdx = random.Next(0, scenarioList.Count);

            dbtsoList = scenarioList[randomIdx];
        }
        else
        {
            dbtsoList = scenarioList[mood - 1];
        }





        dbtListLength = dbtsoList.dbtList.Count;

        Time.timeScale = 1f;
        instance = this;


        SpawnStone();

        for (int i = 0; i < numOfInitFoods; i++)
        {//generate some random foods when started
            SpawnFood(i);
        }


        HP = MaxHP;
        //uiManager.UpdateScore(totalScore);

        gameover = false;



        uiManager.ShowScenario(dbtsoList.mood, dbtsoList.scenario);
    }





    private void InitDBTListData()//clear the existing data and load the data in specific language
    {
        moodIdx = new string[scenarioList.Count];
        for (int i = 0; i < scenarioList.Count; i++)
        {
            moodIdx[i] = scenarioList[i].mood;
        }

        //clear the list of all DBTSOList
        foreach (DBTSOList dbtlist in scenarioList)
        {
            dbtlist.dbtList.Clear();
        }


        string[] csvRecord = csvReader.ReadCSV("snake2dTextData");
        //int numOfCol = 6;

        int languageIndex = PlayerPrefs.GetInt("Lanaguage");//0: TradChi, 1: SimpChi, 2: ENG
        //total 191 records
        for (int i = 0; i < csvRecord.Length; i++)
        {
            if (csvRecord[i].Trim() == "")
                break;

            string[] columnData = csvRecord[i].Split(',');


            //Debug.Log(i + ": " + csvData[i]);
            //create new DBT data and add into list

            DBTData data = new DBTData();

            //DBTData data = new DBTData();
            data.text = columnData[0 + 3 * languageIndex].Trim();
            //Debug.Log(columnData[0 + 3 * languageIndex].Trim());
            data.category = columnData[1 + 3 * languageIndex].Trim();
            //Debug.Log(columnData[1 + 3 * languageIndex].Trim());
            //data.mood = csvData[i + 2];

            int listIndex = Array.IndexOf(moodIdx, columnData[2 + 3 * languageIndex].Trim());//find corresponding DBTSO List by mood
            //Debug.Log(columnData[2 + 3 * languageIndex].Trim());
            //Debug.Log(listIndex);
            scenarioList[listIndex].dbtList.Add(data);
        }
    }



    private void FixedUpdate()
    {
        HP -= Time.fixedDeltaTime;
        uiManager.SetHPValue(HP / MaxHP);

        string msg;

        switch (PlayerPrefs.GetInt("Language"))
        {
            case 0:
                msg = "你的貪食蛇肚子太餓了";
                break;
            case 1:
                msg = "你的贪食蛇肚子太饿了";
                break;
            default:
                msg = "Your snake is too hungry";
                break;
        }


        if (HP <= 0)
            EndGame(msg);


        timer += Time.fixedDeltaTime;
        uiManager.UpdateTimer(timer);
    }

    public void SpawnStone()//spawn stone randomly
    {
        for (int i = 0; i < 8; i++)
        {
            long timestamp = (long)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Create a new instance of Random with the timestamp as the seed
            var random = new System.Random((int)timestamp + i);

            // Generate a random number between 0 and dbtListLength
            int randomIdx = random.Next(0, dbtListLength);

            Vector3Int randomPosition = GetRandomPosition();

            Instantiate(stonePrefab, randomPosition, Quaternion.identity);
        }
    }

    public void SpawnFood(int seed = 0)//spawn a new food randonly
    {
        long timestamp = (long)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Create a new instance of Random with the timestamp as the seed
        var random = new System.Random((int)timestamp + seed);

        // Generate a random number between 0 and dbtListLength
        int randomIdx = random.Next(0, dbtListLength);
        //int randomIdx = UnityEngine.Random.Range(0, dbtListLength);

        Vector3Int randomPosition = GetRandomPosition();

        Food food = Instantiate(foodPrefabs, randomPosition, Quaternion.identity).GetComponent<Food>();
        food.SetDBTData(dbtsoList.dbtList[randomIdx]);
    }

    private Vector3Int GetRandomPosition()
    {
        Vector3Int randomPosition;
        do
        {
            randomPosition = new Vector3Int(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height), 0);
            //should not spawn on player's position
        } while (Vector3.Distance(randomPosition, player.transform.position) < 5);
        //while(randomPosition == player.transform.position)

        return randomPosition;
    }

    public void EndGame(string message)
    {
        if (gameover)
            return;

        gameover = true;
        Time.timeScale = 0f;
        /*
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameoverClip);

        Thread.Sleep(1000);
        */

        //Debug.Log("Lose: " + message);
        AddRecordToDB(totalScore, timer);
        uiManager.ShowGameOverPanel(message, totalScore, timer, collectedFoodList);
    }

    public void AddHP(float amount)
    {
        HP += amount;
        HP = Mathf.Clamp(HP, 0, MaxHP);
    }

    public void EatFood(string name, int score, string category)
    {
        SpawnFood();
        if (category != Food.categoryList.Last())
        {
            AddHP(score / 5);
            totalScore += score;
            uiManager.ShowEffectText(score);
            uiManager.DecreaseHPAnim("increase");
        }
        else
        {
            AddHP(-score / 5);
            uiManager.DecreaseHPAnim("decrease");
        }

        uiManager.UpdateScore(totalScore);
        CollectedFoodData foodData = new CollectedFoodData(name, timer, score, category);
        collectedFoodList.Add(foodData);
        uiManager.UpdateCollectedList(foodData);
    }


    public void AddRecordToDB(int score, float seconds)//call API to send data to DB
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string timeStr = time.ToString(@"mm\:ss");
        int totalScore = ((int)timer + score);

        if (DatabaseHandler.DB_instance == null)
        {
            Debug.Log("DB not collected");
            return;
        }
        //realmController.AddRecord(playerName, score, timeStr, totalScore);
        DatabaseHandler.DB_instance.AddRecordToDB(playerName, score, timeStr, totalScore);
    }
}

public class CollectedFoodData
{
    public string foodName;
    public string time;
    public int score;
    public string category;

    public CollectedFoodData(string foodName, float seconds, int score, string category)
    {
        this.foodName = foodName;
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        this.time = time.ToString(@"mm\:ss");
        this.score = score;
        this.category = category;
    }
}
