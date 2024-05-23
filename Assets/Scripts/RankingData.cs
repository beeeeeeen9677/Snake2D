using TMPro;
using UnityEngine;

public class RankingData : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private TextMeshProUGUI totalScore;


    public void SetData(string name, int score, string time, int totalScore)
    {
        this.name.text = name;
        this.score.text = score.ToString();
        this.time.text = time;
        this.totalScore.text = totalScore.ToString();
    }
}
