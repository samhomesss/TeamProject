using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yb {
    public class CameraController : MonoBehaviour {
        //ī�޶� ��ġ ������ 
        Vector3 DestPos;
        [SerializeField]private GameObject _player;

        private void Start() {
            //���� ��ġ�� ����
           DestPos = transform.position;
            //�÷��̾�� ī�޶� �Ҵ�
            _player.GetComponent<PlayerController>().SetCamera(GetComponent<Camera>());  
        }
        private void LateUpdate() {
            //ī�޶��� ��ġ�� �÷��̾� ��ġ + ���� ��ġ�� ����ȭ
           transform.position = _player.transform.position + DestPos;
        }

        
    }

}
