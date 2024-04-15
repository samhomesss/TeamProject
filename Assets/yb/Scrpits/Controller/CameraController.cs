using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class CameraController : MonoBehaviour {
        //ī�޶� ��ġ ������ 
        private readonly Vector3 DestPos = new Vector3(0f,15f,-15f);
        [SerializeField]private GameObject _player;

        private void Start() {
            //�÷��̾�� ī�޶� �Ҵ�
            _player.GetComponent<PlayerController>().SetCamera(GetComponent<Camera>());  
        }
        private void LateUpdate() {
            //ī�޶��� ��ġ�� �÷��̾� ��ġ + ���� ��ġ�� ����ȭ
           if(_player != null)
            transform.position = _player.transform.position + DestPos;
        }

        
    }

}
