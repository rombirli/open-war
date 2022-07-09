using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


namespace Save
{
    public class ChunkSaver : MonoBehaviour, ISaver
    {
        private GameObject _chunk;
        private static readonly Random Random = new();
        public int X { get; set; }
        public int Y { get; set; }
        public int Index { get; private set; }

        public void Save(string _)
        {
            PlayerPrefs.SetInt(Path, Index);
            _chunk.GetComponent<ISaver>()?.Save(Path);
        }

        public bool Load(string _)
        {
            if (X == 0 && Y == 0)
                return true;
            var position = new Vector3(ChunkLoader.ChunkWidth * X, ChunkLoader.ChunkHeight * Y, 0);
            if (!PlayerPrefs.HasKey(Path))
            {
                // generate random chunk
                Index = Random.Next(0, ChunkLoader.Chunks.Length);
                _chunk = Instantiate(ChunkLoader.Chunks[Index], position, Quaternion.identity);
                AddAvailableChunk();
            }
            else
            {
                // load existing chunk
                Index = PlayerPrefs.GetInt(Path);
                _chunk = Instantiate(ChunkLoader.Chunks[Index], position, Quaternion.identity);
            }

            _chunk.transform.parent = transform;
            return _chunk.GetComponent<ISaver>().Load(Path);
        }

        private string Path => $"{GameManager.CurrentGame.Path}/CHUNK:{X},{Y}";

        private void AddAvailableChunk() =>
            PlayerPrefs.SetString($"{GameManager.CurrentGame.Path}/AVAILABLE_CHUNKS",
                string.Join(";",
                    GetAvailableChunks(GameManager.CurrentGame).Concat(new[] { Tuple.Create(X, Y) })
                        .Select(tuple => $"{tuple.Item1},{tuple.Item2}")));

        private static List<Tuple<int, int>> GetAvailableChunks(Game game)
        {
            var s = PlayerPrefs.GetString($"{game.Path}/AVAILABLE_CHUNKS", "");
            if (s.Length == 0)
                return new List<Tuple<int, int>>();
            return s.Split(';').Select(x =>
                Tuple.Create(int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1]))
            ).ToList();
        }

        public static void RemoveGameChunks(Game game)
        {
            GetAvailableChunks(game).ForEach(tuple =>
            {
                var path = $"{game.Path}/CHUNK:{tuple.Item1},{tuple.Item2}";
                PlayerPrefs.DeleteKey(path);
            });
        }
    }
}