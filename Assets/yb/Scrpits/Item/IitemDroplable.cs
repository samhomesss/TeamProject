using UnityEngine;
namespace yb {
    /// <summary>
    /// 아이템 드랍 관련 인터페이스
    /// </summary>
    public interface IItemDroplable {
        void Set(string item);  //드랍할 아이템 세팅
        void Drop(Vector3 pos);  //세팅한 아이템 드랍
    }
}
