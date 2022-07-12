using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity = 10;
    public GameObject explosionPrefab;
    public bool ignorePlayer = false;
    public bool ignoreEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * velocity * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collectable") ||
            col.gameObject.CompareTag("Player") && ignorePlayer ||
            col.gameObject.CompareTag("Enemy") && ignoreEnemy) return;
        if (explosionPrefab != null)
            Instantiate(explosionPrefab,
                transform.position,
                explosionPrefab.transform.rotation);
        Destroy(gameObject);
    }
}