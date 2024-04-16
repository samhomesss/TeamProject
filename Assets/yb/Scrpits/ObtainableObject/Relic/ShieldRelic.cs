using Photon.Pun;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using yb;

namespace yb
{
    public class ShieldRelic : ObtainableObject, IRelic
    {

        public string Name => gameObject.name;
        private void Start() => _photonView = GetComponent<PhotonView>();
        public Define.RelicType RelicType { get; } = Define.RelicType.ShieldRelic;

        public Transform MyTransform => transform;

        public void DeleteRelic(PlayerController player)
        {
            player.PickupController.DeleteRelic(this);
            player.HaveRelicNumber--;
            //player.DestroyRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.DeleteRelic(this), () => player.HaveRelicNumber--);
        }

        public override void Pickup(PlayerController player)
        {
            SetRelic(player);
            player.HaveRelicNumber++;
        }

        [PunRPC]
        public override void PickupPhoton(int playerViewId)
        {
            Debug.Log("ΩØµÂ∑º∏Ø ∏‘¿Ω");
            PlayerController player;
            player = PhotonNetwork.GetPhotonView(playerViewId).GetComponent<PlayerController>();
            player.PickupController.SetRelic(this);
            player.HaveRelicNumber++;


            SetRelic(player);


            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Destroy(gameObject);
        }

        public void SetRelic(PlayerController player)
        {
            #region «ˆ¿Á ªÁøÎ æ»«‘
            //if (IsTestMode.Instance.CurrentUser == Define.User.Hw)
            //{
            //    Debug.Log("IsTestMode.Instance.CurrentUser == Define.User.Hw");
            //    //player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => { });
            //    player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () =>
            //    {
            //        if (PhotonNetwork.IsMasterClient)
            //            PhotonNetwork.Destroy(gameObject);//∂•ø° ∂≥æÓ¡¯ ø¿∫Í¡ß∆Æ ªË¡¶
            //    });
            //}
            //else
            //{
            //   Debug.Log("else");
            //   player.SetRelicEvent?.Invoke(RelicType.ToString(), () => player.PickupController.SetRelic(this), () => Managers.Resources.Destroy(gameObject));
            //}
            #endregion
            player.ChangeRelicIMGEvent.Invoke(RelicType.ToString(), () => { }, () => { });

        }

        public override void ShowName(PlayerController player)
        {
            base.ShowName(player);
        }

        public override void HideName()
        {
            base.HideName();
        }
    }
}
