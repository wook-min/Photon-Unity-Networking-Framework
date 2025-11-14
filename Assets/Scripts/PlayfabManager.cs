using UnityEngine;
using Photon.Pun;
using PlayFab;
using UnityEngine.UI;
using Photon.Realtime;
using PlayFab.ClientModels;
using System.Collections;
using TMPro;

public class PlayfabManager : MonoBehaviourPunCallbacks
{

    [SerializeField] private InputField addressInputField;
    [SerializeField] private InputField passwardInputField;

    public void Success(LoginResult loginResult)
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";

        StartCoroutine(Connect());
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
        PannelManager.Instance.Load(Panel.ERROR, playFabError.GenerateErrorReport());
        Debug.Log(playFabError.GenerateErrorReport());
    }

}
