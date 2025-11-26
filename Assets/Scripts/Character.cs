using Photon.Pun;
using UnityEngine;

public class Character : MonoBehaviourPun
{
    [Header("카메라 관련")]
    [SerializeField] private Camera remoteCamera;
    [SerializeField] private CharacterController controller;

    [Header("캐릭터 움직임 관련")]
    [SerializeField] private float speed = 3f;
    [SerializeField] Vector3 direction;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        DisableCamera();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Control();
            Move();
        }
    }


    public void Control()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        // direction 방향을 단위 벡터로 설정합니다.
        direction.Normalize();
    }

    public void Move()
    {
        controller.Move(direction * speed * Time.deltaTime);
    }

    public void DisableCamera()
    {
        // 현재 플레이어가 나 자신이라면
        if (photonView.IsMine)
        {
            Camera.main.gameObject.SetActive(false);
        }
        else // 원격 객체라면
        {
            remoteCamera.gameObject.SetActive(false);
        }
    }
}
