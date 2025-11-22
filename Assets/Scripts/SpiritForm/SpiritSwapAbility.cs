using UnityEngine;

public class SpiritSwapAbility : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpiritFormController controller;
    [SerializeField] private SpiritSpawner spawner;

    [Header("Input")]
    [Tooltip("Key used to swap the body with the spirit while in spirit form.")]
    public KeyCode swapKey = KeyCode.F;
    public SpiritFormController spiritForm;


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip swapSound;

    private void Awake()
    {
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(swapKey))
        {
            TrySwap();
        }
    }

    
    public void TrySwap()
    {
        if (controller == null || spawner == null)
            return;

        
        if (!controller.IsInSpirit)
        {
            Debug.Log("[SpiritSwapAbility] Tried to swap, but not in SpiritActive state.");
            return;
        }

        
        if (!controller.HasSwapUnlocked)
        {
            Debug.Log("[SpiritSwapAbility] Tried to swap, but swap is not unlocked yet.");
            return;
        }

        GameObject spirit = spawner.CurrentSpirit;
        if (spirit == null)
        {
            Debug.Log("[SpiritSwapAbility] Tried to swap, but spirit instance is null.");
            return;
        }

        // Swap positions
        Vector3 bodyPos = controller.transform.position;
        Vector3 spiritPos = spirit.transform.position;

        controller.transform.position = spiritPos;
        spirit.transform.position = bodyPos;
        if (audioSource && swapSound)
        {
            audioSource.pitch = 2.0f;
            audioSource.PlayOneShot(swapSound, 2.0f);
            audioSource.pitch = 1f;
        }

        spiritForm.ExitSpirit();

        Debug.Log("[SpiritSwapAbility] Swapped body and spirit positions.");
    }
}
