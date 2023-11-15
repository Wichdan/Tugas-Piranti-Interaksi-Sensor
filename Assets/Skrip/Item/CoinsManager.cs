using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int _coins, _bestCoins;
    [SerializeField] TextMeshProUGUI _coinsCount;

    private HeartSystem _playerHeart;

    public static CoinsManager Instance;

    public int BestCoins { get => _bestCoins; set => _bestCoins = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerHeart = GameObject.Find("Player").GetComponent<HeartSystem>();
    }

    private void Update()
    {
        _coinsCount.text = "X " + _coins.ToString("00");
        GetSomething();
    }

    public void AddCoins(int add)
    {
        _coins += add;
        _bestCoins += add;
    }

    public void GetSomething()
    {
        if (_coins >= 100)
        {
            _playerHeart.Heal(2);
            _coins = 0;
        }
    }
}
