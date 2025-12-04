using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreatePanel : MonoBehaviourPunCallbacks
{
    [Header("입력 인풋필드")]
    [SerializeField] private TMP_InputField roomName;
    [SerializeField] private TMP_InputField roomPW;

    [Header("방인원 토글")]
    [SerializeField] private Toggle count2;
    [SerializeField] private Toggle count3;
    [SerializeField] private Toggle count4;

    [Header("생성 및 취소")]
    [SerializeField] private Button create;
    [SerializeField] private Button cancle;

    [Header("선택 이미지 색깔")]
    [SerializeField] private Color select = new();
    [SerializeField] private Color unSelect = new();

    private Image count2Image;
    private Image count3Image;
    private Image count4Image;

    private bool isInit = false;

    public override void OnEnable()
    {
        base.OnEnable();

        if (!isInit)
        {
            Init();

            count2.onValueChanged.AddListener(isOn => OnImageChange(isOn, count2Image));
            count3.onValueChanged.AddListener(isOn => OnImageChange(isOn, count3Image));
            count4.onValueChanged.AddListener(isOn => OnImageChange(isOn, count4Image));

            forceVisual();

            create.onClick.AddListener(() => OnCreateRoom());
            cancle.onClick.AddListener(() => Cancel());
            isInit = true;
        }
    }

    private void OnDestroy()
    {
        count2.onValueChanged.RemoveAllListeners();
        count3.onValueChanged.RemoveAllListeners();
        count4.onValueChanged.RemoveAllListeners();

        create.onClick.RemoveAllListeners();
        cancle.onClick.RemoveAllListeners();
    }

    public void Init()
    {
        if (count2.TryGetComponent<Image>(out var image2))
        {
            count2Image = image2;
        }

        if (count3.TryGetComponent<Image>(out var image3))
        {
            count3Image = image3;
        }

        if (count4.TryGetComponent<Image>(out var image4))
        {
            count4Image = image4;
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        PannelManager.Instance.Load(Panel.ERROR, message);
    }

    public void OnImageChange(bool isOn, Image image)
    {
        if (isOn)
        {
            image.color = select;
        }
        else
        {
            image.color = unSelect;
        }
    }

    public int playerCount()
    {
        int count = 2;
        if (count2.isOn == true)
            count = 2;
        else if (count3.isOn == true)
            count = 3;
        else if (count3.isOn == true)
            count = 4;

        return count;
        
    }

    public void OnCreateRoom()
    {
        // 방의 이름, 오픈 여부, 활성화 여부, 접속할 수 있는 최대 인원
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = playerCount();
        roomOptions.IsOpen = true;

        if (roomPW.text.Count() >= 1)
        {
            roomOptions.IsOpen = false;
        }

        roomOptions.IsVisible = true;

        PhotonNetwork.CreateRoom(
            roomName.text, roomOptions);

        gameObject.SetActive(false);
    }


    public void Cancel()
    {
        gameObject.SetActive(false);
    }

    public void forceVisual()
    {
        count2.SetIsOnWithoutNotify(true);
        count3.SetIsOnWithoutNotify(false);
        count4.SetIsOnWithoutNotify(false);

        OnImageChange(true, count2Image);
        OnImageChange(false, count3Image);
        OnImageChange(false, count4Image);
    }
}
