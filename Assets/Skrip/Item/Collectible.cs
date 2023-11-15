using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool _isGet;
    private void Update()
    {
        if (_isGet)
        {
            transform.Translate(Vector2.up * 5 * Time.deltaTime);
            Destroy(gameObject, 0.3f);
        }
    }

    public virtual void GetSomething()
    {
        _isGet = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetSomething();
        }
    }
}
