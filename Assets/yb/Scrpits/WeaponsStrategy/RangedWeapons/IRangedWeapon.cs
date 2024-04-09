using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace yb {
    /// <summary>
    /// ��� ���� ���� �������̽�
    /// </summary>
    public interface IRangedWeapon {
        public Define.WeaponType WeaponType { get; set; }  //������ Ÿ��

        public Vector3 DefaultScale { get; set; }  //���� ������Ʈ�� �⺻ ũ��
        void Shot(Vector3 targetPos,  PlayerController player);  //�߻� �Լ�

        void OnUpdateRelic(PlayerController player);  //���� ���� ��, ���� ȿ�� ������Ʈ

        void Reload(PlayerController player);  //���� ������

        bool CanReload();  //���Ⱑ ������ �����Ѱ�?

        void OnUpdate();  //���� ���� Update�Լ�

        bool CanShot();  //�߻簡 �����Ѱ�?

    }
}
