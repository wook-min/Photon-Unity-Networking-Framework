using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class MasterManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float spawnTime = 5f;

    private WaitForSeconds wait;

    public bool IsRunning { get; private set; } = true;

    private Coroutine currentCo;


    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            currentCo = StartCoroutine(SpawnBall());
        }
    }

    private IEnumerator SpawnBall()
    {
        wait = new(spawnTime);

        while (IsRunning)
        {
            if (PhotonNetwork.CurrentRoom != null)
                PhotonNetwork.InstantiateRoomObject("Ball", Vector3.zero, Quaternion.identity);

            yield return wait;
        }
    }

    private void OnDestroy()
    {
        if (currentCo != null)
        {
            StopCoroutine(currentCo);
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
