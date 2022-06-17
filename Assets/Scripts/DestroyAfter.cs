using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyAfter = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    private float _aliveTime = 0;

    // Update is called once per frame
    void Update()
    {
        _aliveTime += Time.deltaTime;
        if (_aliveTime >= destroyAfter) Destroy(gameObject);
    }
}