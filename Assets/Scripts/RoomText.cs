using Photon.Pun;
using Photon.Realtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomCount;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private Button button;
    [SerializeField] private RoomInfo roomInfo;

    public event Action OnEntered;

    private void Awake()
    {
        OnEntered += EnterRoom;
    }

    public void OnConnectRoom()
    {
        PhotonNetwork.JoinRoom(roomName.text);
    }

    private void OnDestroy()
    {
        OnEntered -= EnterRoom;
    }

    // LobbyManager에서 호출하는 함수
    public void UpdateRoomText(RoomInfo info)
    {
        this.roomInfo = info;
        roomName.text = info.Name;
        roomCount.text = $"[{info.PlayerCount}/{info.MaxPlayers}]";
        OnEntered?.Invoke();
    }

    public void EnterRoom()
    {
        if (roomInfo.IsOpen)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
            
    }
}
