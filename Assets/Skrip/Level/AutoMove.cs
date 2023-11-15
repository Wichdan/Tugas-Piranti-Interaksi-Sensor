using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 4f;

    private void Update()
    {
        transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
    }
}
