using UnityEngine;

public class SecretAreaUnlocker : MonoBehaviour
{
    public GameObject secretAreaRoot;   // the COVER tilemap object or parent

    public void UnlockSecretArea()
    {
        Debug.Log("SecretAreaUnlocker.UnlockSecretArea() called");

        if (secretAreaRoot == null)
        {
            Debug.LogError("SecretAreaUnlocker: secretAreaRoot is NOT assigned!");
            return;
        }

        Debug.Log("Disabling: " + secretAreaRoot.name);
        secretAreaRoot.SetActive(false);
    }
}
