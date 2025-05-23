using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityToReact : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SendDataToReact(string message);

    public void SendMessageToReact(string msg)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendDataToReact(msg);
#else
        Debug.Log("Mensaje a React: " + msg);
#endif
    }
}