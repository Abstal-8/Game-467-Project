using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    public TextMeshProUGUI TextMessage;

    public void SetMessage(string msg)
    {
        Debug.Log("SetMessage called on " + gameObject.name + " with: " + msg);

        if (TextMessage == null)
        {
            Debug.LogWarning("TextMessage is NULL on " + gameObject.name);
            return;
        }

        TextMessage.text = msg;
    }
}
