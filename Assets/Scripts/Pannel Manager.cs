using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum Panel
{
    ERROR,
    SUBSCRIBE,
    RoomCreatePanel,
    Pause
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
            if (this.panel == null || this.panel.Equals(null))
            {
                Debug.LogWarning($"[PannelManager] {panel} 패널이 Destroy 상태라 재생성합니다.");
                panelDict.Remove(panel);
                this.panel = null;
            }

            // 키값을 못찾은 경우
            this.panel = (GameObject)Instantiate(Resources.Load(panel.ToString()));

            this.panel.name = this.panel.name.Replace("(Clone)", "");
            /*
            string name = this.panel.gameObject.name;

            int index = name.IndexOf('(');

            string newName = name.Remove(index, 7);

            this.panel.gameObject.name = newName;
            */

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
