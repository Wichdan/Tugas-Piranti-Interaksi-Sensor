using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGround : MonoBehaviour
{
    [SerializeField] float _destroyTime = 20f;

    private void Start()
    {
        Destroy(gameObject, _destroyTime);
    }
}
