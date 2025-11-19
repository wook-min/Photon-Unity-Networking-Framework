using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomCount;
    [SerializeField] private TextMeshProUGUI roomName;

    public void OnConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    public void UpdateRoomText(RoomInfo info)
    {
        roomName.text = info.Name;
        roomCount.text = $"[{info.PlayerCount}/{info.MaxPlayers}]";
    }
}
