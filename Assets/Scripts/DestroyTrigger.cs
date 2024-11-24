using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField] private Cannon cannon;
    private int count;

    private void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            
            count++;
            if (count >= cannon.startAmmo)
            {
                count = 0;
                cannon.End();
            }
        }
    }
}
