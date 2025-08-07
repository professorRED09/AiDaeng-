using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaengLevelSelect : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private Vector3 _direction;

    private bool _isMoving = false;
    private bool isFacingRight = true;
    private Vector3 _targetPos;
    [SerializeField] private float _moveSpeed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //transform.position = 
    }

    private void Update()
    {
        // check if daeng is at the target position
        if(transform.position != _targetPos)
        {
            _isMoving = true;

            _direction = (_targetPos - transform.position).normalized;

            transform.position += _moveSpeed * Time.deltaTime * _direction;

            // Check if player reached the waypoint
            if (Vector3.Distance(transform.position, _targetPos) < 0.1f)
            {
                transform.position = _targetPos;                         
            }
        }
        else
        {
            _isMoving = false;            
        }

        Animate();

        if (_direction.x > 0 && !isFacingRight || _direction.x < 0 && isFacingRight)
        {
            Flip();
        }

    }

    public void SetWaypoint(Vector2 transform)
    {
        _targetPos = transform;
    }

    void Animate()
    {
        _animator.SetBool("isMoving", _isMoving);
        _animator.SetFloat("moveX", _direction.x);
        _animator.SetFloat("moveY", _direction.y);
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        if (!_isMoving && scale.x < 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
        else
        {
            scale.x *= -1;
            transform.localScale = scale;
            isFacingRight = !isFacingRight;
        }
            
        
        
    }
}
