using Photon.Pun;
using UnityEngine;

public class CreateManager : MonoBehaviourPunCallbacks
{
    [Header("생성 위치 및 회전")]
    [SerializeField] private Vector3 position;
    [SerializeField] private Quaternion rotation;

    private void Start()
    {
        PhotonNetwork.Instantiate("Character", position, rotation);
    }
}
