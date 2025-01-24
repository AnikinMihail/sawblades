
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sawblade : MonoBehaviour
{
    private const string JUMPED_OVER = "JumpedOver";
    private const string DESTROY = "Destroy";
    
    
    [SerializeField] private Transform disabled;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private GameObject coin;

    private Rigidbody2D _rigidbody;
    private Player _player;
    
    private bool _isSelected;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = Player.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        _rigidbody.rotation += 10;
        if (_rigidbody.position.y > 7)
        {
            Destroy(gameObject);
        }

        if (_player.transform.position.y < -4f && _isSelected)
        {
            _isSelected = false;
            ScoreManager.instance.AddPoint();
            GameObject currentCoin = Instantiate(coin, transform.position, Quaternion.identity);
            currentCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.value * 3, 2);
            animator.SetBool(DESTROY, true);
            Destroy(gameObject, .2f);
        } 
    }

    public void OnJumpedOver()
    {
        animator.SetBool(JUMPED_OVER, true);
        disabled.gameObject.SetActive(true);
        Debug.Log("JumpedOver");
        _isSelected = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CinemachineShake.Instance.ShakeCamera(.2f, .1f);
        dust.Play();
        if (other.gameObject.layer == (int)Player.Layer.Player)
        {   
            MainMenu.Stop();
        }
    }
}
