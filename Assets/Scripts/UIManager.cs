using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private Image HPMask;
    float HPOriginalSize;
    [SerializeField]
    private Animator hpAnim;
    [SerializeField]
    private TMPro.TextMeshProUGUI timerText;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreText;
    [SerializeField]
    private Transform foodList;
    [SerializeField]
    private GameObject effectTxtPrefab;
    [SerializeField]
    private GameObject foodDataPrefab;
    private List<GameObject> collectedFoodList = new List<GameObject>();
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private Text gameOverMsg;
    [SerializeField]
    private ResultPanel resultPanel;
    [SerializeField]
    private GameObject UIResultFoodDataPrefab;
    [SerializeField]
    private Transform resultContentPanel;
    [SerializeField]
    private Text scenario;

    //briefing part
    [SerializeField]
    private Briefing briefing;



    private void Awake()
    {
        instance = this;
        HPOriginalSize = HPMask.rectTransform.rect.width;
        UpdateScore(0);

    }

    public void SetHPValue(float value)
    {
        HPMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, HPOriginalSize * value);
    }

    public void UpdateTimer(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);

        string str = time.ToString(@"mm\:ss");

        timerText.text = str;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void ShowEffectText(int amount)
    {
        GameObject effectTxt = Instantiate(effectTxtPrefab, scoreText.transform);
        effectTxt.GetComponent<TextMeshProUGUI>().text = "+" + amount;
    }

    public void UpdateCollectedList(CollectedFoodData foodData)
    {
        //show the first 5 newest collected foods
        GameObject newFoodData = Instantiate(foodDataPrefab, foodList);
        string foodText = foodData.foodName.Substring(0, Math.Min(foodData.foodName.Length, 7));
        if (foodData.foodName.Length > 6)
            foodText += "...";

        newFoodData.GetComponent<Text>().text = foodData.time + "\t" + foodText;

        if (foodData.category == Food.categoryList.Last())
        {
            newFoodData.GetComponent<Text>().color = Color.red;
        }

        collectedFoodList.Add(newFoodData);
        //collectedFoodList.Add(foodData.time + ": " + foodData.foodName);
        if (collectedFoodList.Count > 5)
        {
            GameObject removedObject = collectedFoodList[0];
            collectedFoodList.RemoveAt(0);
            Destroy(removedObject);
        }
    }

    //Game Over
    public void ShowGameOverPanel(string msg, int totalScore, float seconds, List<CollectedFoodData> foodList)
    {
        gameOverPanel.SetActive(true);
        gameOverMsg.text = msg;
        resultPanel.SetResultTxt(totalScore, seconds);
        ShowResult(foodList);
        transform.Find("ControlBtnPanel").gameObject.SetActive(false);
    }

    private void ShowResult(List<CollectedFoodData> foodList)//show all collected foods and its data after GameOver
    {

        RectTransform rt = resultContentPanel.GetComponent<RectTransform>();
        //Debug.Log(UIResultFoodDataPrefab.GetComponent<RectTransform>().rect.height);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (foodList.Count) * UIResultFoodDataPrefab.GetComponent<RectTransform>().rect.height);
        //Debug.Log(UIResultFoodDataPrefab.GetComponent<RectTransform>().rect.height);


        foreach (CollectedFoodData foodData in foodList)
        {
            UIResultFoodData UIFoodData = Instantiate(UIResultFoodDataPrefab, resultContentPanel).GetComponent<UIResultFoodData>();
            UIFoodData.SetText(foodData.time, foodData.foodName, foodData.category);
        }
    }

    public void ShowScenario(string mood, string sText)
    {
        briefing.StartCountDown(mood, sText);


        scenario.text = "情绪：" + mood + "\n";
        //scenario.text = sText;
        StartCoroutine(StartShowingScenario(sText));
    }

    IEnumerator StartShowingScenario(string sText)
    {
        for (int i = 0; i < sText.Length; i++)
        {
            StartCoroutine(GenerateRandomChar(sText[i], i + 6));
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator GenerateRandomChar(char finalChar, int index)
    {
        scenario.text += finalChar;
        StringBuilder sb;
        for (int j = 0; j < 5; j++)
        {
            sb = new StringBuilder(scenario.text);
            sb[index] = Convert.ToChar(UnityEngine.Random.Range(0, 26) + 65);
            scenario.text = sb.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        sb = new StringBuilder(scenario.text);
        sb[index] = finalChar;
        scenario.text = sb.ToString();
    }

    public void DecreaseHPAnim(string triggerStr)
    {
        hpAnim.SetTrigger(triggerStr);
    }
}
