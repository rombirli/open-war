using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public float frequency;
    public GameObject gameObject;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 1f) <= Time.deltaTime * frequency)
            Instantiate(gameObject, transform.position, transform.rotation);
    }
}