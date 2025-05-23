using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private void Update()
    {
        GameManager.Instance.timeSurvived += Time.deltaTime;
    }
}
