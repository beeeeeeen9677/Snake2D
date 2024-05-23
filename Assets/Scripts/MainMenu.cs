using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private InputField inputField;
    private string playerName;
    public void OnStartBtnPressed()
    {
        if (!ValiadateInput())
            return;

        PlayerPrefs.SetString("PlayerName", playerName);
        SceneManager.LoadScene(1);
    }

    private bool ValiadateInput()
    {
        playerName = inputField.text.Trim();
        if (playerName == "")
        {
            playerName = "Anonymous";
            return true;
        }
        return true;
    }
}
