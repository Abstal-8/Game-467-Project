using UnityEngine;

public class PlayerKeyRing : MonoBehaviour
{
    // Simple flag: does the player have the cat key?
    public bool HasCatKey { get; private set; } = false;

    public void GiveCatKey()
    {
        HasCatKey = true;
        Debug.Log("[KeyRing] Player picked up cat key.");
    }

    public void UseCatKey()
    {
        HasCatKey = false;
        Debug.Log("[KeyRing] Player used cat key.");
    }
}
