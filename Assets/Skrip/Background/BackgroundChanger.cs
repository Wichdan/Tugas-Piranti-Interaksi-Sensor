using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] GameObject[] _backgrounds;

    private void Update()
    {
        if (GameManager.Instance.LenghtCount >= 53)
        {
            SetBackground(1);
        }
    }

    void SetBackground(int index)
    {
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            if(index == i)
                _backgrounds[i].SetActive(true);
            else
                _backgrounds[i].SetActive(false);
        }
    }
}
