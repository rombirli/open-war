using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class ChunkLoader : MonoBehaviour
{
    public GameObject[] chunks;
    private readonly Random random = new();

    public float chunkWidth = 6.5f, chunkHeight = 4;
    private readonly Dictionary<Tuple<int, int>, GameObject> _cache = new();

    // Update is called once per frame
    private int lastIntersectionX = 0, lastIntersectionY = 0;

    private void Start()
    {
        _cache.Add(new Tuple<int, int>(0, 0), new GameObject("Empty chunk - spawn"));
        UpdateChunks(0, 0);
    }

    void Update()
    {
        // 0 - 14,14 
        var playerPos = GameObject.FindWithTag("Player").transform.position;
        var x = (int)(playerPos.x / chunkWidth - (playerPos.x < 0 ? 1 : 0));
        var y = (int)Math.Round(playerPos.y / chunkHeight - .5);
        if (x == lastIntersectionX && y == lastIntersectionY) return;
        lastIntersectionX = x;
        lastIntersectionY = y;
        UpdateChunks(x, y);
    }

    private void UpdateChunks(int intersectionX, int intersectionY)
    {
        foreach (var chunk in _cache.Values) chunk.SetActive(false);
        for (int i = 0; i < 2; i++)
        for (int j = 0; j < 2; j++)
            LoadChunk(intersectionX + i, intersectionY + j);
    }

    private void LoadChunk(int x, int y)
    {
        GameObject chunkToLoad;
        var coord = new Tuple<int, int>(x, y);
        if (!_cache.TryGetValue(coord, out chunkToLoad))
        {
            if (chunks.Length == 0) return;
            var position = new Vector3(chunkWidth * x, chunkHeight * y, 0);
            chunkToLoad = Instantiate(chunks[random.Next(0, chunks.Length)], position, Quaternion.identity);
            _cache.Add(coord, chunkToLoad);
        }

        chunkToLoad.SetActive(true);
    }
}