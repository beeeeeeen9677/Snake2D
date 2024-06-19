using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIResultFoodData : MonoBehaviour
{
    //[SerializeField]private TMPro.TextMeshProUGUI timeTxt;
    [SerializeField]
    private Text nameTxt;
    [SerializeField]
    private Text categoryTxt;


    public void SetText(string time, string name, string category)
    {
        //timeTxt.text = time;
        nameTxt.text = time + "\t" + name;
        categoryTxt.text = category;
        if (category == Food.categoryList.Last())
        {
            categoryTxt.color = Color.red;
        }
    }

}
