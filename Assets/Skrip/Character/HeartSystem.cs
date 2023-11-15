using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    [Header("Heart Value")]
    [SerializeField] private int _heart = 1; 
    [SerializeField] private int _maxHeart = 3;

    [Header("Invicible")]
    [SerializeField] private bool _isInvicible = false;
    [SerializeField] private float _invicibleTime = 0.0f;
    [SerializeField] GameObject _invicibleMask;

    [Header("Other")]
    [SerializeField] private AudioClip _hurtClip;

    private int _isDefeatHash, _isHurtHash;

    Animator _anim;

    Collider2D _currentCollider;

    public int Heart { get => _heart; set => _heart = value; }
    public int MaxHeart { get => _maxHeart; set => _maxHeart = value; }

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _isDefeatHash = Animator.StringToHash("isDefeat");
        _isHurtHash = Animator.StringToHash("Hurt");

        _heart = _maxHeart;
        _currentCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        if(_isInvicible) return;
        _heart -= damage;

        if(_hurtClip != null)
            AudioSource.PlayClipAtPoint(_hurtClip, transform.position);

        _anim.SetTrigger(_isHurtHash);
        SetInvicible();
        Defeat();
    }

    void Defeat()
    {
        if (_heart <= 0)
        {
            _anim.SetBool(_isDefeatHash, true);
            _heart = 0;
            _currentCollider.enabled = false;
            Destroy(gameObject, 1f);
        }
    }

    void SetInvicible()
    {
        _isInvicible = true;

        if(_invicibleMask != null)
            _invicibleMask.SetActive(true);
        
        StartCoroutine(DisableInvicible());
    }

    IEnumerator DisableInvicible()
    {
        yield return new WaitForSeconds(_invicibleTime);
        _isInvicible = false;

        if(_invicibleMask != null)
            _invicibleMask.SetActive(false);
    }

    public void Heal(int heal)
    {
        if (_heart >= _maxHeart)
        {
            _heart = _maxHeart;
            return;
        }

        _heart += heal;
    }
}
