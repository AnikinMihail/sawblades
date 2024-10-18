using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public bool makeGhost;
    
    [SerializeField] private GameObject ghost;
    [SerializeField] private GameObject visualObject;
    
    [SerializeField] private float ghostDelay;
    
    private float ghostDelaySeconds;

    private void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    private void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0f)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.transform.rotation = visualObject.transform.rotation;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
