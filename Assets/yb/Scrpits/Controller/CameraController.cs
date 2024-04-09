using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class CameraController : MonoBehaviour {
        //카메라 위치 조정용 
        Vector3 DestPos;
        [SerializeField]private GameObject _player;

        private void Start() {
            //시작 위치를 저장
           DestPos = transform.position;
            //플레이어에게 카메라 할당
            _player.GetComponent<PlayerController>().SetCamera(GetComponent<Camera>());  
        }
        private void LateUpdate() {
            //카메라의 위치를 플레이어 위치 + 시작 위치로 동기화
           transform.position = _player.transform.position + DestPos;
        }

        
    }

}
