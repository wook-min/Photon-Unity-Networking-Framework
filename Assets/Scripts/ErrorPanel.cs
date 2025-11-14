using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] Text text;

    public void SetMessage(string msg)
    {
        text.text = msg;
    }
}
