using Photon.Pun;
using UnityEngine;

public class Head : MonoBehaviourPunCallbacks
{
    [Header("회전 최소 최대각")]
    [SerializeField] private float minAngle = -65f;
    [SerializeField] private float maxAngle = 65f;
    [SerializeField] private Rotation rotation;

    [Header("회전 속도 및 각")]
    [SerializeField] private float axis;
    [SerializeField] private float speed = 2f;

    private void Awake()
    {
        rotation = GetComponent<Rotation>();
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            rotation.RotateX(minAngle, maxAngle);
        }
    }



}
