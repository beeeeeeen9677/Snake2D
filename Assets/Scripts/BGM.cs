using UnityEngine;

public class BGM : MonoBehaviour
{
    private static BGM instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        DontDestroyOnLoad(gameObject);

        int muted = PlayerPrefs.GetInt("Muted");
        AudioListener.volume = muted == 0 ? 1 : 0;
    }
}
