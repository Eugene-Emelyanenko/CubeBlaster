using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float bounceFactor = 1.5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Vector2 normal = collision.contacts[0].normal;
            Vector2 newDirection = Vector2.Reflect(rb.velocity.normalized, normal);

            rb.velocity = speed / 1.5f * (newDirection + normal * bounceFactor) + new Vector2(0f, 0.25f);
        }
    }
}
