using System.Collections;
using UnityEngine;
// If URP 2D lights are available, this type will resolve; if not, file still compiles because it's only referenced by 'as' cast.
using UnityEngine.Rendering.Universal;  // Safe to include if URP is installed

public class CandleGlow : MonoBehaviour
{
    [Header("Links")]
    public SpiritForm spirit;                 // Drag your Player's SpiritForm here
    public Behaviour lightComponent;          // Drag ONE of: Light2D (URP) OR Light (3D)
    public SpriteRenderer glowSprite;         // Optional: a soft round sprite child for fallback

    [Header("Glow When In Spirit")]
    public Color onColor = new Color(0.4f, 1f, 0.7f, 1f); // minty green
    public float onIntensity = 2.0f;          // 2D light intensity OR 3D light intensity
    public float onSpriteAlpha = 0.8f;        // fallback sprite alpha

    [Header("Glow When Not In Spirit")]
    public Color offColor = new Color(1f, 0.9f, 0.7f, 1f); // warm candle
    public float offIntensity = 0.3f;         // dim candle baseline
    public float offSpriteAlpha = 0.15f;

    [Header("Optional Flicker")]
    public bool flicker = true;
    public float flickerSpeed = 12f;
    public float flickerAmount = 0.2f;        // percent of intensity

    // internal
    private Light _light3D;                   // built-in light
    private Light2D _light2D;                 // URP 2D light
    private Coroutine _flickerCo;

    void Awake()
    {
        if (lightComponent != null)
        {
            _light3D = lightComponent as Light;
            _light2D = lightComponent as Light2D;
        }
    }

    void OnEnable()
    {
        if (spirit != null)
            spirit.OnSpiritStateChanged += HandleSpiritChanged;
    }

    void OnDisable()
    {
        if (spirit != null)
            spirit.OnSpiritStateChanged -= HandleSpiritChanged;

        if (_flickerCo != null)
            StopCoroutine(_flickerCo);
    }

    void Start()
    {
        // Initialize to current state
        ApplyState(spirit != null && spirit.IsInSpirit);
    }

    private void HandleSpiritChanged(bool inSpirit)
    {
        ApplyState(inSpirit);
    }

    private void ApplyState(bool inSpirit)
    {
        // Choose targets
        var targetColor = inSpirit ? onColor : offColor;
        var targetIntens = inSpirit ? onIntensity : offIntensity;
        var targetAlpha = inSpirit ? onSpriteAlpha : offSpriteAlpha;

        // 2D light path
        if (_light2D != null)
        {
            _light2D.color = targetColor;
            _light2D.intensity = targetIntens;
        }
        // 3D light path
        else if (_light3D != null)
        {
            _light3D.color = targetColor;
            _light3D.intensity = targetIntens;
        }

        // Sprite fallback
        if (glowSprite != null)
        {
            var c = targetColor;
            c.a = targetAlpha;
            glowSprite.color = c;
        }

        // Flicker
        if (flicker)
        {
            if (_flickerCo != null) StopCoroutine(_flickerCo);
            _flickerCo = StartCoroutine(FlickerRoutine(targetIntens));
        }
    }

    private IEnumerator FlickerRoutine(float baseIntensity)
    {
        // Works for both 2D and 3D light; ignored if neither present
        while (true)
        {
            float noise = (Mathf.PerlinNoise(Time.time * flickerSpeed, 0f) - 0.5f) * 2f;
            float mult = 1f + noise * flickerAmount;

            if (_light2D != null) _light2D.intensity = baseIntensity * mult;
            if (_light3D != null) _light3D.intensity = baseIntensity * mult;

            // Sprite fallback “intensity” via alpha tweak
            if (glowSprite != null)
            {
                var c = glowSprite.color;
                c.a = Mathf.Clamp01(c.a * mult);
                glowSprite.color = c;
            }

            yield return null;
        }
    }
}
