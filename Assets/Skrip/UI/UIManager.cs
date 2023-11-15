using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Image[] _heartImage;
    [SerializeField] HeartSystem _heartSystem;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _heartSystem = GameObject.Find("Player").GetComponent<HeartSystem>();
    }

    private void Update()
    {
        UpdateHeart(_heartSystem.Heart);
    }

    void UpdateHeart(int heart)
    {
        for (int i = 0; i < _heartImage.Length; i++)
        {
            if (heart > i)
                _heartImage[i].enabled = true;
            else
                _heartImage[i].enabled = false;
        }
    }
}
