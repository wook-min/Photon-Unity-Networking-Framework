using Photon.Pun;
using System;
using UnityEngine;

public class Mouse : MonoBehaviourPunCallbacks
{

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            SetMouse(false);
        }
        else
        {
            SetMouse(false);
        }
    }


    public void SetMouse(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = (CursorLockMode)Convert.ToInt32(!state);
    }

}
