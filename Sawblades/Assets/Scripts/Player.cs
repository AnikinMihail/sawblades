using System;
using UnityEngine;



public class Player : MonoBehaviour
{
    public static Player instance;
    
    [SerializeField] private Transform playerVisual;
    
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 10f;

    private Rigidbody2D _rb;
    
    private bool _isGrounded;
    private bool _doubleJump;

    private float _horizontalInput;
    private float _yRotation;

    private readonly LayerMask _layerMask = 1 << 10;
    
    public enum Layer
    {
        Default,
        TransparentFX,
        IgnoreRaycast,
        Ground,
        Water,
        UI,
        Sawblades,
        SawbladeCollidable,
        Walls,
        Player
    }
    
    public int score;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");


        _yRotation = _horizontalInput switch
        {
            -1f => 180f,
            1f => 0.0f,
            _ => _yRotation
        };

        Quaternion target = Quaternion.Euler(0, _yRotation, 0);
        playerVisual.rotation = Quaternion.Slerp(playerVisual.rotation, target, Time.deltaTime * 10f); 

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

    private void FixedUpdate()
    {
        Vector2 raycastOrigin = transform.position - new Vector3(0, .6f, 0);
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, 5f);

        while (hit.collider)
        {
            if (hit.transform.TryGetComponent(out Sawblade sawblade))
            {
                sawblade.OnJumpedOver();
                raycastOrigin = sawblade.transform.position - new Vector3(0, .6f, 0);
                hit = Physics2D.Raycast(raycastOrigin, -Vector2.up, 5f);
            }
            else
            {
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (int)Layer.Ground)
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == (int)Layer.Ground)
        {
            _isGrounded = false;
        }
    }
}
