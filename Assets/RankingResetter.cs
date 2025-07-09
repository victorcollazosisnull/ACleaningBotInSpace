using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingResetter : MonoBehaviour
{
    public UnityToReact unityToReact;

    public void ResetRanking()
    {
        PlayerPrefs.DeleteKey("GameRanking");
        PlayerPrefs.Save();

        Debug.Log("Ranking reseteado");

        // Si quieres notificar a React que se reseteó
        if (unityToReact != null)
        {
            unityToReact.SendMessageToReact("Ranking reseteado");
        }
    }
}
