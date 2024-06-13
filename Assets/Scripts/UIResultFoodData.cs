using UnityEngine;
using UnityEngine.UI;

public class UIResultFoodData : MonoBehaviour
{
    //[SerializeField]private TMPro.TextMeshProUGUI timeTxt;
    [SerializeField]
    private Text nameTxt;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreTxt;

    public void SetText(string time, string name, int score)
    {
        //timeTxt.text = time;
        nameTxt.text = time + "   " + name;
        scoreTxt.text = score.ToString();
    }

}
