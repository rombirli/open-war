using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Save;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class ChunkLoader : MonoBehaviour
{
    public static GameObject[] Chunks;
    public static readonly float ChunkWidth = 14f;
    public static readonly float ChunkHeight = 10f;
    private readonly Dictionary<Tuple<int, int>, GameObject> _cache = new();
    private int _lastIntersectionX = 0, _lastIntersectionY = 0;
    private float _nextOptimization;

    private void Start()
    {
        Chunks = Resources.LoadAll<GameObject>("Chunks/");
        UpdateChunks();
        _nextOptimization = Time.time + 5;
    }

    void Update()
    {
        if (_nextOptimization >= Time.time)
            OptimizeFarChunks();
        UpdateChunks();
    }

    private void UpdateChunks()
    {
        var playerPos = GameObject.FindWithTag("Player").transform.position;
        var x = (int)(playerPos.x / ChunkWidth - (playerPos.x < 0 ? 1 : 0));
        var y = (int)Math.Round(playerPos.y / ChunkHeight - .5);
        if (x == _lastIntersectionX && y == _lastIntersectionY) return;
        _lastIntersectionX = x;
        _lastIntersectionY = y;
        
        foreach (var chunk in _cache.Values) chunk.SetActive(false);
        for (int i = 0; i < 2; i++)
        for (int j = 0; j < 2; j++)
            LoadChunk(x + i, y + j);
    }

    private void LoadChunk(int x, int y)
    {
        GameObject chunk;
        var coord = new Tuple<int, int>(x, y);
        if (!_cache.TryGetValue(coord, out chunk))
        {
            chunk = new GameObject();
            chunk.AddComponent<ChunkSaver>();
            chunk.GetComponent<ChunkSaver>().X = x;
            chunk.GetComponent<ChunkSaver>().Y = y;
            chunk.GetComponent<ChunkSaver>().Load($"CHUNK:{x},{y}");
            _cache.Add(coord, chunk);
        }
        chunk.transform.parent = transform;
        chunk.SetActive(true);
    }

    private void OptimizeFarChunks()
    {
        var chunksToRemove = new List<Tuple<int, int>>();
        foreach (var ((x, y), _) in _cache)
            if (Math.Abs(x - _lastIntersectionX) > 3 || Math.Abs(y - _lastIntersectionY) > 3)
                chunksToRemove.Add(new Tuple<int, int>(x, y));
        foreach (var chunkPosition in chunksToRemove)
        {
            Destroy(_cache[chunkPosition]);
            _cache.Remove(chunkPosition);
        }
    }
}