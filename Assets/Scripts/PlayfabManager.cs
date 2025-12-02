using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private InputField addressInputField;
    [SerializeField] private InputField passwardInputField;

    public string nickName;

    public void Success(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), Success, Failure);

        StartCoroutine(Connect());
    }

    public void Success(GetAccountInfoResult getAccountInfoResult)
    {
        PhotonNetwork.LocalPlayer.NickName = getAccountInfoResult.AccountInfo?.Username;
    }

    private IEnumerator Connect()
    {
        // Name server에서 Master Server로 넘어가는 중...
        PhotonNetwork.ConnectUsingSettings(); // 마스터 서버로 접속하는 함수

        // 서버 연결이 완료되거나 시간 초과될 때까지 대기
        while (PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;
        }

        // 특정 로비를 생성하여 진입하는 함수
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = addressInputField.text,
            Password = passwardInputField.text
        };

        // 콜백으로 성공, 실패를 반환하는 로그인 시도 함수
        PlayFabClientAPI.LoginWithEmailAddress
            (request, Success, Failure);
    }

    public void Failure(PlayFabError playFabError)
    {
        PannelManager.Instance.Load(Panel.ERROR, FailureMessage(playFabError));
        Debug.Log(playFabError.GenerateErrorReport());
    }

    public void SignUp()
    {
        PannelManager.Instance.Load(Panel.SUBSCRIBE, "");
        Debug.Log("회원가입 중...");
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
            case PlayFabErrorCode.InvalidEmailOrPassword:
            case PlayFabErrorCode.InvalidUsernameOrPassword:
            case PlayFabErrorCode.AccountNotFound:
                return "로그인 실패: 이메일 또는 비밀번호가 틀렸습니다.";

            case PlayFabErrorCode.AccountBanned:
                return "계정이 정지되었습니다.";

            case PlayFabErrorCode.InvalidEmailAddress:
                return "이메일 형식이 올바르지 않습니다.";

            default:
                return $"기타 오류: {playFabError.Error}";
        }
    }

}
