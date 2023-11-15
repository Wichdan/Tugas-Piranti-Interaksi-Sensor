using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardEnemy : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 2f;
    [SerializeField] bool _isFacingRight = true;

    [Header("Check Gap")]
    [SerializeField] private float _checkGapRadius = 0.2f;
    [SerializeField] private Transform _checkGapPos, _checkForward;
    [SerializeField] private LayerMask _checkGapLayer;

    [Header("Other")]
    [SerializeField] private GameObject _stompPos;
    Rigidbody2D _rb2d;
    HeartSystem _enemyHeart;

    private void Start()
    {
        //_anim = GetComponentInChildren<Animator>();
        _rb2d = GetComponent<Rigidbody2D>();
        _enemyHeart = GetComponent<HeartSystem>();

        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), true);
    }

    private void FixedUpdate()
    {
        if (_enemyHeart.Heart <= 0)
        {
            _rb2d.velocity = new Vector2(0, _rb2d.velocity.y);
            Destroy(_stompPos);
            return;
        }
        MoveForward();
        CheckGap();
        CheckForward();
    }

    void MoveForward()
    {
        if (_isFacingRight)
            _rb2d.velocity = new Vector2(_moveSpeed, _rb2d.velocity.y);
        else
            _rb2d.velocity = new Vector2(-_moveSpeed, _rb2d.velocity.y);
    }

    void CheckGap()
    {
        Collider2D checkGap = Physics2D.OverlapCircle(_checkGapPos.position, _checkGapRadius, _checkGapLayer);
        if (!checkGap)
            FlipCharacter();
    }

    void CheckForward()
    {
        bool isCheckForward = Physics2D.OverlapCircle(_checkForward.position, _checkGapRadius, _checkGapLayer);
        if (isCheckForward)
            FlipCharacter();
    }

    public void FlipCharacter()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            FlipCharacter();
    }

    private void OnDrawGizmos()
    {
        if (_checkGapPos != null)
            Gizmos.DrawWireSphere(_checkGapPos.position, _checkGapRadius);

        if (_checkForward != null)
            Gizmos.DrawWireSphere(_checkForward.position, _checkGapRadius);
    }
}
