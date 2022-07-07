using Unity.VisualScripting;
using UnityEngine;

namespace Save
{
    public class ChunkSaver : MonoBehaviour
    {
        public void Save(int x, int y, int chunkIndex)
        {
            string path = $"CHUNK:{x},{y}";
            PlayerPrefs.SetInt(path, chunkIndex);
            int i = 0;
            foreach (var savable in transform.GetComponentsInChildren<Savable>())
                savable.Save($"{path}/OBJ:{i++}");
        }

        public static bool Load(int x, int y, out GameObject chunk, out int index)
        {
            var path = $"CHUNK:{x},{y}";
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
    }
}