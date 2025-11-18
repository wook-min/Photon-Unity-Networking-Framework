using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Dictionary<string, GameObject> roomDict = new();

    public event Action OnDestroyRoom; // 룸 파괴 시 이벤트
    public event Action OnChangedRoom; // 룸 정보 변경 시 이벤트
    public event Action OnCreateRoom;  // 룸 생성 시 이벤트

    public void Create(string roomName)
    {
        if (roomDict.ContainsKey(roomName))
        {
            Debug.LogError($"{roomName} is Already Exist!");
            return;
        }

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach (var room in roomList)
        {
            room.
        }
    }
}
