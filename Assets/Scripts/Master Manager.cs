using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float spawnTime = 5f;

    private WaitForSeconds wait;

    public bool IsRunning { get; private set; } = true;


    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnBall());
        }
    }

    private IEnumerator SpawnBall()
    {
        wait = new(spawnTime);

        while (IsRunning)
        {
            PhotonNetwork.InstantiateRoomObject("Ball", Vector3.zero, Quaternion.identity);

            yield return wait;
        }
    }

    public void StopSpawn()
    {
        IsRunning = false;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
        StartCoroutine(SpawnBall());
        Debug.Log(PhotonNetwork.PlayerList[0]);
    }
}
