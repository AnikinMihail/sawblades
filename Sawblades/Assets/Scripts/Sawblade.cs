
using System;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    [SerializeField] private Transform disabled;

    private Rigidbody2D _rigidbody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        _rigidbody.rotation += 10;
        if (_rigidbody.position.y > 7)
        {
            Destroy(gameObject);
        }
    }

    public void OnJumpedOver()
    {
        disabled.gameObject.SetActive(true);
    }
}
