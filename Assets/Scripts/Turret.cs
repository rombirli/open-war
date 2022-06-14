using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float delayBetweenShoots = 1;
    public float viewField = 3;

    public GameObject canon;

    // Update is called once per frame
    void Update()
    {
        var playerPos = GameObject.FindWithTag("Player").transform.position;
        if (GetComponent<Hitable>().healthPoints > 0)
            if ((transform.position - playerPos).magnitude < viewField)
                transform.up = (playerPos - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (GetComponent<Hitable>().healthPoints <= 0)
            return;

        RaycastHit2D hit = Physics2D.Raycast(canon.transform.position, transform.up);
        if (hit.collider != null && hit.collider.gameObject != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
                tryShoot();
        }
    }

    private float _nextShoot = 0;

    void tryShoot()
    {
        if (_nextShoot > Time.time) return;
        _nextShoot = Time.time + delayBetweenShoots;
        Instantiate(bulletPrefab,
            canon.transform.position,
            transform.rotation);
    }
}