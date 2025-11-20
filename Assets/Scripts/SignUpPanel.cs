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
        PannelManager.Instance.Load(Panel.ERROR, FailureMessage(result));
        userName.text = "";
        userID.text = "";
        userPassward.text = "";
    }

    public string FailureMessage(PlayFab.PlayFabError playFabError)
    {
        string result = "";

        var detail = playFabError.ErrorDetails;

        if (detail != null)
        {
            if (detail.ContainsKey("Username"))
            {
                result += "올바르지 못한 유저 이름입니다.";
            }

            if (detail.ContainsKey("Email"))
            {
                result += "\n올바르지 못한 이메일 형식입니다.";
            }

            if (detail.ContainsKey("Password"))
            {
                result += "\n올바르지 못한 비밀번호 형식입니다.(6자리 이상)";
            }

            return result;
        }

        switch (playFabError.Error)
        {
            case PlayFab.PlayFabErrorCode.InvalidEmailOrPassword:
            case PlayFab.PlayFabErrorCode.InvalidUsernameOrPassword:
            case PlayFab.PlayFabErrorCode.AccountNotFound:
                return "로그인 실패: 이메일 또는 비밀번호가 틀렸습니다.";

            case PlayFab.PlayFabErrorCode.AccountBanned:
                return "계정이 정지되었습니다.";

            case PlayFab.PlayFabErrorCode.InvalidEmailAddress:
                return "이메일 형식이 올바르지 않습니다.";

            default:
                return $"기타 오류: {playFabError.Error}";
        }
    }
}
