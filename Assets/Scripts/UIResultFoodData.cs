using UnityEngine;

public class UIResultFoodData : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI timeTxt;
    [SerializeField]
    private TMPro.TextMeshProUGUI nameTxt;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreTxt;

    public void SetText(string time, string name, int score)
    {
        timeTxt.text = time;
        nameTxt.text = name;
        scoreTxt.text = score.ToString();
    }

}
