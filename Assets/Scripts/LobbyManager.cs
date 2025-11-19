using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Dictionary<string, GameObject> roomDict = new();
    [SerializeField] private Transform content;

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject prefab = null;

        foreach (var roomInfo in roomList)
        {
            // 룸이 삭제된 경우
            if (roomInfo.RemovedFromList)
            {
                roomDict.TryGetValue(roomInfo.Name, out prefab);

                Destroy(prefab);

                roomDict.Remove(roomInfo.Name);
            }
            else // 룸의 정보가 변경되는 경우
            {
                // 룸이 처음 생성되는 경우
                if (!roomDict.ContainsKey(roomInfo.Name))
                {
                    GameObject clone = Instantiate(Resources.Load<GameObject>("Room"), content);
                    roomDict.Add(roomInfo.Name, clone);
                }

                roomDict.TryGetValue(roomInfo.Name, out var pf);

                if (pf.TryGetComponent<RoomText>(out var rt))
                {
                    rt.UpdateRoomText(roomInfo);
                }
            }
            
        }
    }
}
