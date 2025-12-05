using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PausePanel : MonoBehaviourPunCallbacks
{
    [Header("버튼 관련")]
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;


    private void Start()
    {
        continueButton.onClick.AddListener(Continue);
        quitButton.onClick.AddListener(Quit);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        MouseManager.Instance.SetMouse(true);
    }

    public override void OnDisable()
    {
        MouseManager.Instance.SetMouse(false);
    }

    private void OnDestroy()
    {
        continueButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
    }


    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    { 
        PhotonNetwork.LeaveRoom();
        gameObject.SetActive(false);
        MouseManager.Instance.SetMouse(true);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
