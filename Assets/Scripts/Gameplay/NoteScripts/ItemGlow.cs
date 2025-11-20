using System.Collections;
using UnityEngine;
// If URP 2D lights are available, this type will resolve; if not, file still compiles because it's only referenced by 'as' cast.
using UnityEngine.Rendering.Universal;  // Safe to include if URP is installed

public class ItemGlow : MonoBehaviour
{
    public Light myLight; // Assign the Light component in the Inspector
    
    private void Start()
    {
        if (myLight != null)
        {
            myLight.enabled = false;
        }
    }
    // Makes the light orangey
    public void inInteraction()
    {
        if (myLight != null)
        {
            myLight.range = 10f;
            myLight.color = new Color(1f, 0.5f, 0f); // Orange color
            myLight.enabled = true; // Ensure light is on
        }
    }

    // Makes the light white
    public void outInteraction()
    {
        if (myLight != null)
        {
            myLight.range = 10f;
            myLight.color = Color.white; // White color
            myLight.enabled = true; // Ensure light is on
        }
    }

    // Turns the light off
    public void TurnLightOff()
    {
        if (myLight != null)
        {
            myLight.enabled = false;
        }
    }
}
