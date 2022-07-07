using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Save;
using UnityEngine;

public class TransformSaver : MonoBehaviour, Savable
{
    public void Save(string path)
    {
        var transform1 = transform;
        var euler = transform1.rotation.eulerAngles;
        var position = transform1.position;
        PlayerPrefs.SetString(path,
            $"{position.x},{position.y},{position.z};{euler.x},{euler.y},{euler.z}");
    }

    public void Load(string path)
    {
        var parts = PlayerPrefs.GetString(path).Split(";");
        var position = parts[0].Split(",").Select(float.Parse).ToArray();
        var euler = parts[1].Split(",").Select(float.Parse).ToArray();
        transform.position = new Vector3(position[0], position[1], position[2]);
        transform.rotation = Quaternion.Euler(euler[0], euler[1], euler[2]);
    }
}