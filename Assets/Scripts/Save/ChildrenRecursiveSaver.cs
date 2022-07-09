using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Save
{
    public class ChildrenRecursiveSaver : MonoBehaviour, ISaver
    {
        public void Save(string path)
        {
            var i = 0;
            foreach (var savable in transform.GetComponentsInChildren<ISaver>())
                if (savable is MonoBehaviour && ((MonoBehaviour)savable).transform.parent == transform)
                    savable.Save(ChildPath(path, i++));
        }

        public bool Load(string path)
        {
            var i = 0;
            Debug.Log(path);
            foreach (var savable in transform.GetComponentsInChildren<ISaver>())
                if (savable is MonoBehaviour && ((MonoBehaviour)savable).transform.parent == transform)
                    if (!savable.Load(ChildPath(path, i++)))
                        return false;
            return true;
        }

        private static string ChildPath(string root, int i) => $"{root}/OBJ:{i}";
    }
}