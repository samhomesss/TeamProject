using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace yb {
    /// <summary>
    /// 모든 무기 관련 인터페이스
    /// </summary>
    public interface IRangedWeapon {
        public Define.WeaponType WeaponType { get; set; }  //무기의 타입

        public Vector3 DefaultScale { get; set; }  //무기 오브젝트의 기본 크기
        void Shot(Vector3 targetPos,  PlayerController player);  //발사 함수

        void OnUpdateRelic(PlayerController player);  //무기 변경 시, 렐릭 효과 업데이트

        void Reload(PlayerController player);  //무기 재장전

        bool CanReload();  //무기가 재장전 가능한가?

        void OnUpdate();  //무기 관련 Update함수

        bool CanShot();  //발사가 가능한가?

    }
}
