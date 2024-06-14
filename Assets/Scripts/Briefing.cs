using System;
using UnityEngine;
using UnityEngine.UI;

public class Briefing : MonoBehaviour
{
    [SerializeField]
    private Text mood;

    [SerializeField]
    private Text scenario;

    [SerializeField]
    private TMPro.TextMeshProUGUI cdText;

    [SerializeField]
    private float cdTime = 5;

    private bool startCD = false;

    public void StartCountDown(string mood, string scenario)
    {
        gameObject.SetActive(true);

        Time.timeScale = 0;
        this.mood.text = mood;
        this.scenario.text = scenario;

        startCD = true;

        //Debug.Log("called");
    }

    private void Update()
    {

        if (!startCD)
            return;


        cdTime -= Time.unscaledDeltaTime;
        cdText.text = Math.Ceiling(cdTime).ToString();

        //Debug.Log(cdTime);


        if (cdTime < 0)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

}
