using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputActionReference actionRef;

    public override void OnJoinedRoom()
    {
        actionRef.action.Enable();
        actionRef.action.performed += OnPerformed;
    }

    private void OnDestroy()
    {
        actionRef.action.performed -= OnPerformed;
        actionRef.action.Disable();
    }

    private void OnPerformed(InputAction.CallbackContext cx)
    {
        if (!photonView.IsMine) return;
        PannelManager.Instance.Load(Panel.Pause, "");
        Debug.Log("패널 생성");
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers <= PhotonNetwork.CurrentRoom.PlayerCount)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log(PhotonNetwork.CurrentRoom.IsOpen);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.MaxPlayers > PhotonNetwork.CurrentRoom.PlayerCount)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
            Debug.Log(PhotonNetwork.CurrentRoom.IsOpen);
        }
    }

    


}
