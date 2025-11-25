using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateManager : MonoBehaviourPunCallbacks
{
    [Header("생성 위치 및 회전")]
    [SerializeField] private List<Transform> positionList = new();
    [SerializeField] private Quaternion rotation;


    private void Awake()
    {
        Create();
    }

    private void Start()
    {
        SetPosition();
    }

    public void Create()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.MaxPlayers; i++)
        {
            Transform clone = Instantiate(Resources.Load<Transform>("Create Position " + i));

            positionList.Add(clone);
        }
    }

    public void SetPosition()
    {
        int index = (int)PhotonNetwork.CurrentRoom.PlayerCount - 1;
        index = Mathf.Max(index, 0);

        PhotonNetwork.Instantiate("Character", positionList[index].position, rotation);
    }
}
