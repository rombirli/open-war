using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Save
{
    public class SaveRegularly : MonoBehaviour
    {
        public float interval;
        private float _nextSave;

        public void Start()
        {
            _nextSave = Time.time + interval;
            var i = 0;
            foreach (var saver in GetComponents<ISaver>())
                saver.Load(ChildPath(GameManager.CurrentGame.Path, i++));
        }

        public void Update()
        {
            if (Time.time >= _nextSave)
            {
                _nextSave = Time.time + interval;
                Save();
            }
        }

        private void OnDestroy()
        {
            Save();
        }

        private void Save()
        {
            var i = 0;
            foreach (var saver in GetComponents<ISaver>())
                saver.Save(ChildPath(GameManager.CurrentGame.Path, i++));
        }

        private static string ChildPath(string root, int i) => $"{root}/OBJ:{i}";
    }
}