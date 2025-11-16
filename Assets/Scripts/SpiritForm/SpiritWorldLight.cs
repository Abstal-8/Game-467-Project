using UnityEngine;
using UnityEngine.Rendering.Universal;   // needed for Light2D

public class SpiritWorldLight : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpiritFormController controller;
    [SerializeField] private Light2D globalLight;   

    [Header("Lighting")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color spiritColor = new Color(0.7f, 1f, 0.7f); 

    [SerializeField]
    [Range(0f, 2f)]
    private float normalIntensity = 1f;

    [SerializeField]
    [Range(0f, 2f)]
    private float spiritIntensity = 0.9f;

    [Header("Transition")]
    [SerializeField] private bool smoothTransition = true;
    [SerializeField] private float lerpSpeed = 5f;

    private Color targetColor;
    private float targetIntensity;

    private void Awake()
    {
        if (globalLight == null)
            globalLight = GetComponent<Light2D>();

        
    }

    private void OnEnable()
    {
        if (controller != null)
            controller.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        if (controller != null)
            controller.OnStateChanged -= HandleStateChanged;
    }

    private void Start()
    {
       
        HandleStateChanged(controller != null ? controller.CurrentState : SpiritState.BodyFree);

        if (globalLight != null)
        {
            globalLight.color = targetColor;
            globalLight.intensity = targetIntensity;
        }
    }

    private void Update()
    {
        if (!smoothTransition || globalLight == null)
            return;

        globalLight.color = Color.Lerp(globalLight.color, targetColor, lerpSpeed * Time.deltaTime);
        globalLight.intensity = Mathf.Lerp(globalLight.intensity, targetIntensity, lerpSpeed * Time.deltaTime);
    }

    private void HandleStateChanged(SpiritState state)
    {
        bool inSpirit = state == SpiritState.SpiritActive;

        targetColor = inSpirit ? spiritColor : normalColor;
        targetIntensity = inSpirit ? spiritIntensity : normalIntensity;

        
        if (!smoothTransition && globalLight != null)
        {
            globalLight.color = targetColor;
            globalLight.intensity = targetIntensity;
        }
    }
}
