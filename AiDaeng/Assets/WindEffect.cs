using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindEffect : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _currentEffectTimer; 

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Random event for playing wind animation
        _currentEffectTimer -= Time.deltaTime;
        if(_currentEffectTimer <= 0)
        {
            _animator.Play("WindEffect");
            _spriteRenderer.DOFade(1f, 0.5f);
            RandomEffectTime();
        }
    }

    public void HideEffect()
    {
        _spriteRenderer.DOFade(0f, 1f);
    }

    void RandomEffectTime()
    {
        _currentEffectTimer = Random.Range(6f, 8f);
    }
}
