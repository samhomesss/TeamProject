using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class CameraController : MonoBehaviour {
        //카메라 위치 조정용 
        private readonly Vector3 DestPos = new Vector3(0f,15f,-15f);
        [SerializeField]private GameObject _player;

        private void Start() {
            //플레이어에게 카메라 할당
            _player.GetComponent<PlayerController>().SetCamera(GetComponent<Camera>());  
        }
        private void LateUpdate() {
            //카메라의 위치를 플레이어 위치 + 시작 위치로 동기화
           if(_player != null)
            transform.position = _player.transform.position + DestPos;
        }

        
    }

}
