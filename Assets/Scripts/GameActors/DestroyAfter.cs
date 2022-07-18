using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float destroyAfter = 5;
    private float _destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        _destroyTime=Time.time+destroyAfter;
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time>=_destroyTime) Destroy(gameObject);
    }
}