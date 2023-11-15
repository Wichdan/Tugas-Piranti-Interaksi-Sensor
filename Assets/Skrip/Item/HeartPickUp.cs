using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : Collectible
{
    [SerializeField] private int _heartToHeal = 1;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.tag == "Player")
        {
            HeartSystem heartSystem = other.GetComponent<HeartSystem>();
            heartSystem.Heal(_heartToHeal);
        }
    }
}
