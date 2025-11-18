using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.PfEditor.EditorModels;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignUpPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField userName;
    [SerializeField] private TMP_InputField userID;
    [SerializeField] private TMP_InputField userPassward;

    public void Subsribe()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = userName.text,
            Email = userID.text,
            Password = userPassward.text
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, Success, Failure);
    }

    public void Success(RegisterPlayFabUserResult result)
    {
        Debug.Log(result.Username);
        gameObject.SetActive(false);
    }

    public void Failure(PlayFab.PlayFabError result)
    {
        PannelManager.Instance.Load(Panel.ERROR, result.GenerateErrorReport());
        userName.text = "";
        userID.text = "";
        userPassward.text = "";
    }
}
