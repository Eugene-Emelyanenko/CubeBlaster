using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Cannon cannon = FindObjectOfType<Cannon>().GetComponent<Cannon>();
            if (cannon != null)
            {
                cannon.BonusAmmo();
                Destroy(gameObject);
            }
        }
    }
}
