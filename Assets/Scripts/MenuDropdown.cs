using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDropdown : MonoBehaviour
{
    [SerializeField]
    private List<string> moods;
    [SerializeField]
    private DBTSOList[] dbtsoListArr;


    // Start is called before the first frame update
    void Awake()
    {
        ResetDropdownList();//init
    }

    private void ResetDropdownList()
    {
        Dropdown dropdown = GetComponent<Dropdown>();
        dropdown.options.Clear();

        foreach (string mood in moods)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = mood });
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

        dropdown.RefreshShownValue();

        PlayerPrefs.SetInt("Mood", 0);//default
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        //Debug.Log(dropdown.value);
        //Debug.Log(dropdown.options[dropdown.value].text);
        PlayerPrefs.SetInt("Mood", dropdown.value);
    }

    public void updateDropdownList(int index)
    {
        moods.Clear();
        string defaultStr;
        switch (index)
        {
            case 0:
                defaultStr = "請選擇";
                break;
            case 1:
                defaultStr = "请选择";
                break;
            default:
                defaultStr = "Select";
                break;

        }

        moods.Add(defaultStr);//default option
        foreach (DBTSOList dbtsoList in dbtsoListArr)
        {
            moods.Add(dbtsoList.mood);
        }
        ResetDropdownList();
    }
}
