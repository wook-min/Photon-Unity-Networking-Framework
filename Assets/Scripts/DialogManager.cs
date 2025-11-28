using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviourPunCallbacks
{
    [Header("채팅창")]
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject content;
    // [SerializeField] private GameObject 

    private GameObject textPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && canvas.activeSelf == false && photonView.IsMine)
        {
            canvas.SetActive(true);
        }

        if (photonView.IsMine && Input.GetKeyDown(KeyCode.KeypadEnter) && canvas.activeSelf == true
            && EventSystem.current.currentSelectedGameObject != inputfield.gameObject)
        {
            SelectInputField();
        }

        if (photonView.IsMine && Input.GetKeyDown(KeyCode.KeypadEnter) && canvas.activeSelf == true
            && EventSystem.current.currentSelectedGameObject == inputfield.gameObject
            && CheckText())
        {
            InputChat();
        }

        if (photonView.IsMine && Input.GetKeyDown(KeyCode.KeypadEnter) && canvas.activeSelf == true
            && EventSystem.current.currentSelectedGameObject == inputfield.gameObject
            && !CheckText())
        {
            CloseChat();
        }


    }

    public void SelectInputField()
    {
        EventSystem.current.SetSelectedGameObject(inputfield.gameObject);
    }

    public void InputChat()
    {
        if (textPrefab == null)
        {
            textPrefab = Resources.Load<GameObject>("Talk");
        }

        var clone = GameObject.Instantiate(textPrefab, content.transform);
        
        if (clone.TryGetComponent<TextMeshProUGUI>(out var text))
        {
            text.text = $"{PhotonNetwork.NickName}" + " : "+ inputfield.text;
        } 
    }

    public bool CheckText()
    {
        if (inputfield.text != "")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CloseChat()
    {
        canvas.SetActive(false);
    }
}
