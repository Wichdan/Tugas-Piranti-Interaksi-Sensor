using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvicibleMask : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteMask _spriteMask;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteMask = GetComponent<SpriteMask>();
    }

    private void Update()
    {
        _spriteMask.sprite = _spriteRenderer.sprite;
    }
}
