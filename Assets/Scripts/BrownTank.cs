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
    public GameObject flameThrower;
    public List<GameObject> destroyed;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        Inventory.ResetInventory();
    }


    private float _nextShot = 0;
    public GameObject bulletPrefab;
    private static readonly int Turning = Animator.StringToHash("turning");
    private static readonly int Backward = Animator.StringToHash("backward");
    private bool _dead = false;

    // Update is called once per frame
    void Update()
    {
        if (_dead) return;
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
            _nextShot = Math.Max(_nextShot, Time.time + delayBetweenShoots);
        }

        if (Input.GetKey(KeyCode.Space) && Time.time >= _nextShot)
        {
            Shoot();
            _nextShot += delayBetweenShoots;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Explosion"))
        {
            if (!Inventory.Pop(Inventory.Item.Health))
            {
                _dead = true;
                destroyed.ForEach(go => go.SetActive(true));
                flameThrower.SetActive(false);
            }
        }
    }


    private void UpdateTurret()
    {
        if (Camera.main is null) return;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
            ShootTurret(mousePos);
        flameThrower.SetActive(Input.GetMouseButton((int)MouseButton.Right));
        turret.transform.up = (mousePos - turret.transform.position).normalized;
    }

    private void ShootTurret(Vector3 mousePos)
    {
        if (Inventory.Pop(Inventory.Item.TurretAmmo))
            Instantiate(explosionPrefab, mousePos, explosionPrefab.transform.rotation);
    }


    private void Shoot()
    {
        if (Inventory.Pop(Inventory.Item.MainAmmo))
            Instantiate(bulletPrefab,
                canon.transform.position,
                transform.rotation);
    }
}