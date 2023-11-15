using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float _lenght, _startPos;
    [SerializeField] private GameObject _cam;
    [SerializeField] private float _parallaxEffect;

    private void Start()
    {
        _startPos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (_cam.transform.position.x * (1 - _parallaxEffect));
        float distance = (_cam.transform.position.x * _parallaxEffect);

        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);
        if (temp > _startPos + _lenght)
            _startPos += _lenght;
        else if (temp < _startPos - _lenght)
            _startPos -= _lenght;
    }
}
