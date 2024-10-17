using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    private const string JUMPED = "Jumped";
    private const string DOUBLE_JUMPED = "DoubleJumped";
    
    public static Player Instance;
    
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dust;
    
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpSpeed = 10f;

    private Rigidbody2D _rb;
    private TrailRenderer _trail;
    
    private bool _isGrounded;
    private bool _doubleJump;

    private float _horizontalInput;
    private float _yRotation;
    private float _prevYRotation;

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
        Instance = this;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trail = GetComponent<TrailRenderer>();
        _trail.time = 0f;
        animator.SetBool(JUMPED, false);
        animator.SetBool(DOUBLE_JUMPED, false);
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

        if (!_prevYRotation.Equals(_yRotation) && _isGrounded)
        {
            dust.Play();
        }

        _prevYRotation = _yRotation;

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
                if (_isGrounded)
                {
                    dust.Play();
                    animator.SetBool(JUMPED, true);
                }
                if(!_isGrounded && !animator.GetBool(DOUBLE_JUMPED))
                {
                    animator.SetBool(DOUBLE_JUMPED, true);
                    
                }
                _rb.velocity = new Vector2(_rb.velocity.x, jumpSpeed);

                _doubleJump = !_doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * .5f);
        }

        if (!_isGrounded && !_doubleJump)
        {
            _trail.time = 2f;
        }
        else
        {
            _trail.time = 0f;
        }

    }

    private void FixedUpdate()
    {
        Vector2 raycastOrigin = transform.position - new Vector3(0, .6f, 0);
        RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, -Vector2.up);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider)
            {
                if (hit.transform.TryGetComponent(out Sawblade sawblade))
                {
                    sawblade.OnJumpedOver();
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == (int)Layer.Ground)
        {
            _isGrounded = true;
            animator.SetBool(DOUBLE_JUMPED, false);
            animator.SetBool(JUMPED, false);
            
            CinemachineShake.Instance.ShakeCamera(1f, .1f);
            dust.Play();
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
