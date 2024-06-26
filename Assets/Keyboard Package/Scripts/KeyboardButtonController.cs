using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtonController : MonoBehaviour
{
    [SerializeField] Image containerBorderImage;
    [SerializeField] Image containerFillImage;
    [SerializeField] Image containerIcon;
    [SerializeField] TextMeshProUGUI containerText;
    [SerializeField] TextMeshProUGUI containerActionText;

    private void Start()
    {
        SetContainerBorderColor(ColorDataStore.GetKeyboardBorderColor());
        SetContainerFillColor(ColorDataStore.GetKeyboardFillColor());
        SetContainerTextColor(ColorDataStore.GetKeyboardTextColor());
        SetContainerActionTextColor(ColorDataStore.GetKeyboardActionTextColor());
    }

    public void SetContainerBorderColor(Color color) => containerBorderImage.color = color;
    public void SetContainerFillColor(Color color) => containerFillImage.color = color;
    public void SetContainerTextColor(Color color) => containerText.color = color;
    public void SetContainerActionTextColor(Color color)
    {
        containerActionText.color = color;
        containerIcon.color = color;
    }

    public void AddLetter()
    {
        if (KeyboardManager.Instance != null)
        {
            KeyboardManager.Instance.AddLetter(containerText.text);
        }
        else
        {
            Debug.Log(containerText.text + " is pressed");
        }
    }
    public void DeleteLetter()
    {
        if (KeyboardManager.Instance != null)
        {
            KeyboardManager.Instance.DeleteLetter();
        }
        else
        {
            Debug.Log("Last char deleted");
        }
    }
    public void SubmitWord()
    {
        if (KeyboardManager.Instance != null)
        {
            KeyboardManager.Instance.SubmitWord();
        }
        else
        {
            Debug.Log("Submitted successfully!");
        }
    }
}