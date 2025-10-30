using UnityEngine;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    AudioSource audioSource;
    public AudioClip battleTheme;
    public AudioClip explorationTheme;

    public static Action OnBattleMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }

        audioSource = GetComponent<AudioSource>();

    }

    void OnEnable()
    {
        OnBattleMusic += PlayBattleTheme;
        BattleState.battleEnd += PlayExploreTheme;
    }

    void OnDisable()
    {
        OnBattleMusic -= PlayBattleTheme;
        BattleState.battleEnd -= PlayExploreTheme;
    }

    void PlayBattleTheme()
    {
        audioSource.resource = battleTheme;
        audioSource.Play();
    }

    void PlayExploreTheme()
    {
        audioSource.resource = explorationTheme;
        audioSource.Play();
    }

}
