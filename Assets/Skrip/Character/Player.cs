using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Move Config")]
    [SerializeField] private float _sensorValue;
    [SerializeField] private float _jumpPower = 6f;
    [SerializeField] private int _minIdleValue = -5, _maxIdleValue = 5;
    [SerializeField] private float _moveSpeed = 4f;

    [Header("Jump & Stomp Config")]
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer, _stompLayer;
    [SerializeField] private bool isGrounded;

    [Header("Attack")]
    [SerializeField] private bool _isAttack;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 0.2f;
    [SerializeField] private float _attackCountDown = 1f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Audio")]
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private AudioClip _defeatClip;

    private int _isGroundedHash, _isGameStartHash, _isAttackHash;

    private Rigidbody2D _rb2d;
    private Animator _anim;
    private HeartSystem _heartSystem;
    [SerializeField] SerialController _serialController;

    private void Awake()
    {
        _serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _heartSystem = GetComponent<HeartSystem>();

        _isGroundedHash = Animator.StringToHash("isGrounded");
        _isGameStartHash = Animator.StringToHash("isGameStart");
        _isAttackHash = Animator.StringToHash("isAttack");
    }

    private void Update()
    {
        if (_serialController != null)
        {
            GetAndChangeSensorValue();
            JumpInput();
            AttackInput();
        }
        else
        {
            JumpkKeyboardInput();
            AttackKeyboardInput();
        }

        HandleAnimation();

        if (GameManager.Instance.IsStartGame)
        {
            AutoRun();
        }

        if (transform.position.y <= -7)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            _heartSystem.TakeDamage(1);
        }

        if (_heartSystem.Heart <= 0 && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.IsGameOver = true;

            if (_defeatClip != null)
                AudioSource.PlayClipAtPoint(_defeatClip, transform.position);
        }
    }

    private void FixedUpdate()
    {
        GroundAndStompCheck();
    }

    void GetAndChangeSensorValue()
    {
        string message = _serialController.ReadSerialMessage();

        if (message == "__Disconnected__")
            return;

        if (message == null)
            return;

        float messageConvert = float.Parse(message);

        _sensorValue = Mathf.Clamp(messageConvert, -1, 1);

        if (messageConvert <= _maxIdleValue)
            _sensorValue = 0;

        if (messageConvert <= _minIdleValue)
            _sensorValue = -1;
    }

    void AutoRun()
    {
        if (_heartSystem.Heart <= 0)
        {
            _rb2d.velocity = Vector2.zero;
            return;
        }
        _rb2d.velocity = new Vector2(_moveSpeed, _rb2d.velocity.y);
    }

    void JumpInput()
    {
        if (_sensorValue >= 1 && isGrounded)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpPower);
        }
    }

    void AttackInput()
    {
        if (_sensorValue <= -1 && !_isAttack)
        {
            Attack();
        }
    }

    void JumpkKeyboardInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpPower);
        }
    }

    void AttackKeyboardInput()
    {
        if (Input.GetButtonDown("Fire1") && !_isAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        _isAttack = true;
        _weapon.SetActive(_isAttack);
        StartCoroutine(AttackCoundDown());
        SpawnAttackHitBox();
        _anim.SetTrigger(_isAttackHash);

        if (_attackClip != null)
            AudioSource.PlayClipAtPoint(_attackClip, transform.position);
    }

    void SpawnAttackHitBox()
    {
        Collider2D hitBox = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _enemyLayer);
        if (hitBox)
        {
            HeartSystem enemyHearth = hitBox.GetComponent<HeartSystem>();
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hitBox.GetComponent<Collider2D>());
            enemyHearth.TakeDamage(1);
        }
    }

    IEnumerator AttackCoundDown()
    {
        yield return new WaitForSeconds(_attackCountDown);
        _isAttack = false;
        _weapon.SetActive(_isAttack);
    }

    void GroundAndStompCheck()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheckPos.position, _groundRadius, _groundLayer);
        Collider2D stompCol = Physics2D.OverlapCircle(_groundCheckPos.position, _groundRadius, _stompLayer);
        if (stompCol)
        {
            HeartSystem enemyHearth = stompCol.GetComponentInParent<HeartSystem>();
            enemyHearth.TakeDamage(1);
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpPower);
        }
    }

    void HandleAnimation()
    {
        _anim.SetBool(_isGameStartHash, GameManager.Instance.IsStartGame);
        _anim.SetBool(_isGroundedHash, isGrounded);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 6)
        {
            _heartSystem.TakeDamage(1);
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckPos != null)
            Gizmos.DrawWireSphere(_groundCheckPos.position, _groundRadius);

        if (_attackPoint != null)
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }

}
