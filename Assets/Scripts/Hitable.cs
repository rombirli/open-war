using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Hitable : MonoBehaviour
{
    public int healthPoints = 1;
    public GameObject alive;
    public bool removeColliderOnDestroy = false;

    public GameObject dead;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Explosion") && --healthPoints == 0)
            destroy();
    }


    private void destroy()
    {
        alive.SetActive(false);
        dead.SetActive(true);
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        if (removeColliderOnDestroy)
            Destroy(GetComponent<Collider2D>());
    }
}