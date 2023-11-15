using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : Collectible
{
    [SerializeField] int _coinsToGet = 1;
    public override void GetSomething()
    {
        base.GetSomething();
        CoinsManager.Instance.AddCoins(_coinsToGet);
    }
}
