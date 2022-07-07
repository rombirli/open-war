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
    private readonly Random random = new();
    public static GameObject[] Chunks;
    public static readonly float ChunkWidth = 14f;
    public static readonly float ChunkHeight = 10f;
    private readonly Dictionary<Tuple<int, int>, GameObject> _cache = new();
    private readonly Dictionary<Tuple<int, int>, int> _indexes = new();

    private int lastIntersectionX = 0, lastIntersectionY = 0;
    private float nextSave;

    private void Start()
    {
        Chunks = Resources.LoadAll<GameObject>("Chunks/");
        var firstChunk = new GameObject("Empty chunk - spawn");
        firstChunk.AddComponent<ChunkSaver>();
        _cache.Add(new Tuple<int, int>(0, 0), firstChunk);
        _indexes.Add(new Tuple<int, int>(0, 0), -1);
        UpdateChunks(0, 0);
        nextSave = Time.time + 5;
    }

    void Update()
    {
        if (nextSave >= Time.time)
        {
            nextSave = Time.time + 5;
            Save();
            ClearFarChunks();
        }

        var playerPos = GameObject.FindWithTag("Player").transform.position;
        var x = (int)(playerPos.x / ChunkWidth - (playerPos.x < 0 ? 1 : 0));
        var y = (int)Math.Round(playerPos.y / ChunkHeight - .5);
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
        GameObject chunk;
        var coord = new Tuple<int, int>(x, y);
        if (!_cache.TryGetValue(coord, out chunk))
        {
            int index;
            if (!ChunkSaver.Load(GameManager.CurrentGame, x, y, out chunk, out index))
            {
                var position = new Vector3(ChunkWidth * x, ChunkHeight * y, 0);
                index = random.Next(0, Chunks.Length);
                chunk = Instantiate(Chunks[index], position, Quaternion.identity);
                ChunkSaver.AddAvailableChunk(GameManager.CurrentGame,coord);
            }

            if (chunk == null) return;
            _cache.Add(coord, chunk);
            _indexes.Add(coord, index);
        }

        chunk.SetActive(true);
    }

    private void ClearFarChunks()
    {
        var chunksToRemove = new List<Tuple<int, int>>();
        foreach (var ((x, y), _) in _cache)
            if (Math.Abs(x - lastIntersectionX) > 3 || Math.Abs(y - lastIntersectionY) > 3)
                chunksToRemove.Add(new Tuple<int, int>(x, y));
        foreach (var chunkPosition in chunksToRemove)
        {
            _cache.Remove(chunkPosition);
            _indexes.Remove(chunkPosition);
        }
    }

    private void Save()
    {
        foreach (var (pos, chunk) in _cache)
            chunk.GetComponent<ChunkSaver>().Save(GameManager.CurrentGame, pos.Item1, pos.Item2, _indexes[pos]);
    }
}