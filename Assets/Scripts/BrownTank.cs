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
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    private float _nextShoot = 0;
    public GameObject bulletPrefab;
    private static readonly int Turning = Animator.StringToHash("turning");
    private static readonly int Backward = Animator.StringToHash("backward");

    // Update is called once per frame
    void Update()
    {
        GetInputs(out var move, out var rotation);
        TurnAndMove(move, rotation);
        UpdateWheels(move, rotation);
        UpdateTurret();
    }

    private void GetInputs(out float move, out float rotation)
    {
        move = 0;
        rotation = 0;
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
            Shoot();
            _nextShoot += delayBetweenShoots;
        }
    }

    private void TurnAndMove(float move, float rotation)
    {
        var rigidbody2DComponent = GetComponent<Rigidbody2D>();
        rigidbody2DComponent.MoveRotation(rigidbody2DComponent.rotation + Time.deltaTime * turningSpeed * rotation);
        rigidbody2DComponent.MovePosition(transform.position +
                                          move * speed * Time.deltaTime * transform.up);
    }

    private void UpdateWheels(float move, float rotation)
    {
        wheelLeft.GetComponent<Animator>().SetBool(Turning, move != 0 || rotation != 0);
        wheelLeft.GetComponent<Animator>().SetBool(Backward, move < 0 || rotation > 0);
        wheelRight.GetComponent<Animator>().SetBool(Turning, move != 0 || rotation != 0);
        wheelRight.GetComponent<Animator>().SetBool(Backward, move < 0 || rotation < 0);
    }

    private void UpdateTurret()
    {
        if (Camera.main is null) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
            Instantiate(explosionPrefab, mousePos, explosionPrefab.transform.rotation);
        turret.transform.up = (mousePos - turret.transform.position).normalized;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab,
            canon.transform.position,
            transform.rotation);
    }
}