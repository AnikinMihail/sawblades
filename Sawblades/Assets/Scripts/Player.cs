using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 8f;

    private Rigidbody2D _rb;
    
    private bool _isGrounded;
    private bool _doubleJump;

    private float _horizontalInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");

        _rb.velocity = new Vector2(_horizontalInput * moveSpeed, _rb.velocity.y);

        if (_isGrounded && !Input.GetButton("Jump"))
        {
            _doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded || _doubleJump)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);

                _doubleJump = !_doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * .5f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _isGrounded = false;
        }
    }
}
