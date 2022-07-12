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


    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, destroyAfter) <= Time.deltaTime ) Destroy(gameObject);
    }
}