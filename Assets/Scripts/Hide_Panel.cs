using UnityEngine;

public class HideOnBodyState : MonoBehaviour
{
    public SpiritFormController controller;

    void OnEnable()
    {
        if (controller != null)
            controller.OnStateChanged += HandleStateChanged;
    }

    void OnDisable()
    {
        if (controller != null)
            controller.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(SpiritState state)
    {
        // When we are NOT in spirit anymore, hide this panel
        if (state != SpiritState.SpiritActive)
        {
            gameObject.SetActive(false);
        }
    }
}
