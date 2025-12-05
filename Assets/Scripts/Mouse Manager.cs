using Photon.Pun;
using System;
using UnityEngine;

public class MouseManager : MonoBehaviourPunCallbacks
{
    [SerializeField] static MouseManager instance;

    public static MouseManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SetMouse(true);
    }

    public void SetMouse(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }
}
