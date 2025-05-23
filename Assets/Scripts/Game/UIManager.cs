using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI lifeText;

    private int points = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPoint(int amount)
    {
        points += amount;
        pointsText.text = "POINTS: " + points;

        GameManager.Instance.points = points;
    }

    public void UpdateLifeUI(int life)
    {
        lifeText.text = "LIFE: " + life;
    }
}