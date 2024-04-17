using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using yb;

namespace yb {
    public class SpawnObject : MonoBehaviour {
        [SerializeField] private float _ran;
        private string _destructibleObjectPath = "Prefabs/yb/Object/DestructibleObject";
        private DestructibleObject _destructibleObject;
        private Terrain _terrain;
        private Vector3 _defaultPos;

        private void Awake() {
            _terrain = GetComponent<Terrain>();
        }
        private void Start() {
            _defaultPos = transform.position;
            _destructibleObject = Managers.Resources.Load<DestructibleObject>(_destructibleObjectPath);
            Init();
        }

        private void Init() {
            float x = _terrain.terrainData.size.x;
            float z = _terrain.terrainData.size.z;
            float chance = _ran * 0.01f;
            for (int i = 0; i < z; i++) {
                for (int j = 0; j < x; j++) {
                    var ran = Random.Range(0, 1f);
                    if (ran <= chance) {
                        DestructibleObject obj = Instantiate(_destructibleObject).GetComponent<DestructibleObject>();
                        //obj.Init(new Vector3(j, 1f, i) + _defaultPos);
                    }
                }
            }
        }
    }
}

