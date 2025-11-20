using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomCreatePanel : MonoBehaviour
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

    private void OnEnable()
    {
        if (!isInit)
        {
            Init();

            count2.onValueChanged.AddListener(isOn => OnImageChange(isOn, count2Image));
            count3.onValueChanged.AddListener(isOn => OnImageChange(isOn, count3Image));
            count4.onValueChanged.AddListener(isOn => OnImageChange(isOn, count4Image));

            isInit = true;
        }
    }

    private void OnDestroy()
    {
        count2.onValueChanged.RemoveAllListeners();
        count3.onValueChanged.RemoveAllListeners();
        count4.onValueChanged.RemoveAllListeners();
    }

    public void Init()
    {
        if (count2.TryGetComponent<Image>(out var image2))
        {
            count2Image = image2;
        }

        if (count2.TryGetComponent<Image>(out var image3))
        {
            count3Image = image3;
        }

        if (count2.TryGetComponent<Image>(out var image4))
        {
            count4Image = image4;
        }
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
}
