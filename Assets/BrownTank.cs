using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrownTank : MonoBehaviour
{
    public float speed = 1;

    public float turningSpeed = 180;
    public GameObject explosionPrefab;

    public float delayBetweenShoots = 1;
    public GameObject canon;
    public GameObject wheelLeft, wheelRight;
    public GameObject turret;

    // private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        // animator = ;
    }

    private float _nextShoot = 0;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        float rotation = 0, move = 0;
        if (Input.GetKey(KeyCode.A)) rotation++;
        if (Input.GetKey(KeyCode.D)) rotation--;
        if (Input.GetKey(KeyCode.W)) move++;
        if (Input.GetKey(KeyCode.S)) move--;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _nextShoot = Math.Max(_nextShoot, Time.time + delayBetweenShoots);
        }

        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextShoot)
        {
            shoot();
            _nextShoot += delayBetweenShoots;
        }

        if (Input.GetMouseButtonDown((int)MouseButton.Left))
            explodeIt();

        var rigidbody = GetComponent<Rigidbody2D>();
        wheelLeft.GetComponent<Animator>().SetBool("turning", move != 0 || rotation != 0);
        wheelLeft.GetComponent<Animator>().SetBool("backward", move < 0 || rotation > 0);
        wheelRight.GetComponent<Animator>().SetBool("turning", move != 0 || rotation != 0);
        wheelRight.GetComponent<Animator>().SetBool("backward", move < 0 || rotation < 0);
        //animator.SetBool("Walking", move != 0);
        rigidbody.MoveRotation(rigidbody.rotation + Time.deltaTime * turningSpeed * rotation);
        rigidbody.MovePosition(rigidbody.transform.position + move * speed * Time.deltaTime * rigidbody.transform.up);
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        turret.transform.up = (mousePos - turret.transform.position).normalized;
    }

    private void explodeIt()
    {
        var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPos = new Vector3(clickPos.x, clickPos.y, 0);
        Instantiate(explosionPrefab,
            clickPos,
            explosionPrefab.transform.rotation);
    }

    void shoot()
    {
        Instantiate(bulletPrefab,
            canon.transform.position,
            transform.rotation);
    }
}