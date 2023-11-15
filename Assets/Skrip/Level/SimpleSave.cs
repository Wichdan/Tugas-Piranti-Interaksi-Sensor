using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSave : MonoBehaviour
{
    public static SimpleSave Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }
}
