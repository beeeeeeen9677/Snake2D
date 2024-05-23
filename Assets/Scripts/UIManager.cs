using System;
using System.Collections.Generic;
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
    private TMPro.TextMeshProUGUI gameOverMsg;
    [SerializeField]
    private ResultPanel resultPanel;
    [SerializeField]
    private GameObject UIResultFoodDataPrefab;
    [SerializeField]
    private Transform resultContentPanel;

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
        newFoodData.GetComponent<TextMeshProUGUI>().text = foodData.time + "   " + foodData.foodName;
        collectedFoodList.Add(newFoodData);
        //collectedFoodList.Add(foodData.time + ": " + foodData.foodName);
        if (collectedFoodList.Count > 5)
        {
            GameObject removedObject = collectedFoodList[0];
            collectedFoodList.RemoveAt(0);
            Destroy(removedObject);
        }
    }

    public void ShowGameOverPanel(string msg, int totalScore, float seconds, List<CollectedFoodData> foodList)
    {
        gameOverPanel.SetActive(true);
        gameOverMsg.text = msg;
        resultPanel.SetResultTxt(totalScore, seconds);
        ShowResult(foodList);
    }

    private void ShowResult(List<CollectedFoodData> foodList)//show all collected foods and its data after GameOver
    {
        RectTransform rt = resultContentPanel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, (foodList.Count + 1) * 100);
        foreach (CollectedFoodData foodData in foodList)
        {
            UIResultFoodData UIFoodData = Instantiate(UIResultFoodDataPrefab, resultContentPanel).GetComponent<UIResultFoodData>();
            UIFoodData.SetText(foodData.time, foodData.foodName, foodData.score);
        }
    }
}
