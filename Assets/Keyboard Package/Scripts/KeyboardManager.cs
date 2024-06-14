using UnityEngine;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    public static KeyboardManager Instance;
    [SerializeField] Text textBox;
    [SerializeField] InputField printBox;
    [SerializeField]
    private GameObject Keyboard;

    private void Awake()
    {
        Instance = this;
        printBox.text = "";
        textBox.text = "";
    }

    public void DeleteLetter()
    {
        if (textBox.text.Length != 0)
        {
            textBox.text = textBox.text.Remove(textBox.text.Length - 1, 1);
        }
    }

    public void AddLetter(string letter)
    {
        textBox.text = textBox.text + letter;
    }

    public void SubmitWord()
    {
        printBox.text = textBox.text;
        textBox.text = "";
        // Debug.Log("Text submitted successfully!");
        CloseKeyboard();
    }

    public void OpenKeyboard()
    {
        Keyboard.SetActive(true);
        textBox.text = "";
    }

    public void CloseKeyboard()
    {
        Keyboard.SetActive(false);
    }
}
