using System.Collections.Generic;
using System.Linq;
using Gameplay.Shop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GameActors
{
    public class Player : MonoBehaviour
    {
        public float speed = 1;

        public float turningSpeed = 180;
        public GameObject explosionPrefab;

        public float delayBetweenShoots = 1;
        public GameObject canon;
        public GameObject turret;
        public GameObject flameThrower;
        public List<GameObject> destroyed;
        private PlayerInput _input;

        private bool _dead;

        // Start is called before the first frame update
        void Start()
        {
            gameObject.tag = "Player";
            _input = GetComponent<PlayerInput>();
        }


        private float _lastShoot = 0;
        public GameObject bulletPrefab;

        // Update is called once per frame
        void Update()
        {
            if (_dead) return;
            var events=            transform.root.GetComponentsInChildren<EventSystem>().ToArray();
            if (events.Length>1) Debug.Log(events);
            if (Inventory.Health <= 0)
            {
                // player die
                destroyed.ForEach(go => go.SetActive(true));
                Destroy(_input);
                flameThrower.SetActive(false);
                _dead = true;
                return;
            }

            GetInputs(out var move, out var rotation);
            TurnAndMove(move, rotation);
            if (!GameManager.Paused)
                UpdateTurret();
        }

        private void GetInputs(out float move, out float rotation)
        {
            var moveRotation = _input.actions["Move"].ReadValue<Vector2>();
            rotation = moveRotation.x;
            move = moveRotation.y;
            if (_input.actions["SecondaryShoot"].inProgress && Time.time - _lastShoot > delayBetweenShoots)
            {
                Shoot();
                _lastShoot = Time.time;
            }
        }

        private void TurnAndMove(float move, float rotation)
        {
            var rigidbody2DComponent = GetComponent<Rigidbody2D>();
            rigidbody2DComponent.MoveRotation(rigidbody2DComponent.rotation - Time.deltaTime * turningSpeed * rotation);
            rigidbody2DComponent.MovePosition(transform.position +
                                              move * speed * Time.deltaTime * transform.up);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Explosion") && col.gameObject.GetComponent<PlayerTeam>() == null)
            {
                Inventory.Health--;
            }
        }


        private void UpdateTurret()
        {
            if (Camera.main is null) return;
            var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(_input.actions["Look"].ReadValue<Vector2>());
            if (_input.actions["TurretShoot"].triggered)
                ShootTurret(mousePos);
            flameThrower.SetActive(_input.actions["Flamethrower"].inProgress);
            turret.transform.up = (mousePos - (Vector2)turret.transform.position);
        }

        private void ShootTurret(Vector2 direction)
        {
            if (Bag.Quantities[Consumable.MainAmmo] > 0)
                Instantiate(explosionPrefab, direction, explosionPrefab.transform.rotation);
        }


        private void Shoot()
        {
            if (Bag.Quantities[Consumable.MainAmmo] > 0)
            {
                Bag.Quantities[Consumable.MainAmmo]--;
                Instantiate(bulletPrefab,
                    canon.transform.position,
                    transform.rotation);
            }
        }
    }
}