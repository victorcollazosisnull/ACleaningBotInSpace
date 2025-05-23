using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI resultText;

    private void Start()
    {
        int points = GameManager.Instance.points;
        float time = GameManager.Instance.timeSurvived;
        int total = points + Mathf.FloorToInt(time);

        pointsText.text = "POINTS: " + points;
        timeText.text = "TIME: " + Mathf.FloorToInt(time) + "s";
        resultText.text = "RESULT: " + total;
    }
}