
using System;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    [SerializeField] private Transform disabled;
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
            ScoreManager.instance.AddPoint();
            GameObject currentCoin = Instantiate(coin, transform.position, transform.rotation);
            currentCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 3);
            Destroy(gameObject);
        } 
    }

    public void OnJumpedOver()
    {
        disabled.gameObject.SetActive(true);
        _isSelected = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        dust.Play();
        if (other.gameObject.layer == (int)Player.Layer.Player)
        {   
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
