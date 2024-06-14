using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDropdown : MonoBehaviour
{
    [SerializeField]
    private List<string> moods;

    // Start is called before the first frame update
    void Awake()
    {
        Dropdown dropdown = GetComponent<Dropdown>();
        dropdown.options.Clear();

        foreach (string mood in moods)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = mood });
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });

        PlayerPrefs.SetInt("Mood", 0);//default
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        //Debug.Log(dropdown.value);
        //Debug.Log(dropdown.options[dropdown.value].text);
        PlayerPrefs.SetInt("Mood", dropdown.value);
    }
}
