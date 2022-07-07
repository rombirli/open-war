using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Save
{
    public class ChunkSaver : MonoBehaviour
    {
        public void Save(Game game, int x, int y, int chunkIndex)
        {
            string path = $"{game.Path}/CHUNK:{x},{y}";
            PlayerPrefs.SetInt(path, chunkIndex);
            int i = 0;
            foreach (var savable in transform.GetComponentsInChildren<Savable>())
                savable.Save($"{path}/OBJ:{i++}");
        }

        public static bool Load(Game game, int x, int y, out GameObject chunk, out int index)
        {
            var path = $"{game.Path}/CHUNK:{x},{y}";
            if (!PlayerPrefs.HasKey(path))
            {
                chunk = null;
                index = -1;
                return false;
            }

            index = PlayerPrefs.GetInt(path);
            var position = new Vector3(ChunkLoader.ChunkWidth * x, ChunkLoader.ChunkHeight * y, 0);
            chunk = Instantiate(ChunkLoader.Chunks[index], position, Quaternion.identity);
            chunk.transform.position = position;
            var i = 0;
            foreach (var savable in chunk.transform.GetComponentsInChildren<Savable>())
                savable.Load($"{path}/OBJ:{i++}");
            return true;
        }

        public static void AddAvailableChunk(Game game, Tuple<int, int> position) =>
            PlayerPrefs.SetString($"{game.Path}/AVAILABLE_CHUNKS",
                string.Join(";",
                    GetAvailableChunks(game).Concat(new[] { position })
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