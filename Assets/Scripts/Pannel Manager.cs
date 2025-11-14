using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Panel
{
    ERROR,
    SIGNUP
}

public class PannelManager : MonoBehaviour
{
    private static PannelManager instance;
    public static PannelManager Instance => instance;

    private Dictionary<Panel, GameObject> panelDict = new();
    GameObject panel = null;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        DontDestroyOnLoad(gameObject);
    }


    public void Load(Panel panel, string message)
    {
        if (panelDict.TryGetValue(panel, out this.panel) == false)
        {
            // 키값을 못찾은 경우
            this.panel = (GameObject)Instantiate(Resources.Load(panel.ToString()));
            
            panelDict.Add(panel, this.panel);
        }
        else
        {
            this.panel = panelDict[panel];
            this.panel.SetActive(true);
        }

        if (panel == Panel.ERROR)
        {
            if(this.panel.TryGetComponent<ErrorPanel>(out var error))
            {
                error.SetMessage(message);
            }
        }
    }
}
