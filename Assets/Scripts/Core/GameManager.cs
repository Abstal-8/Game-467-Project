using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

//to whom it may concern. the commented out code is the beginnings of the gamestate system. if you wish to continue my work go to (https://www.youtube.com/watch?app=desktop&v=4I0vonyqMi8)
//and then pick up by creating a MenuManager at 5:25 in the video. I am too lazy to finish it now due to the time crunch. I wish you the best of luck. --Luke Bonniwell

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject player;
    public Camera mainCam;
    // public static event Action<GameState> OnGameStateChanged;

    // void Awake()
    // {
    //     Instance = this;
    // }

    // void Start()
    // {
    //     UpdateGameState(GameState.Exploration);
    // }

    // public void UpdateGameState(GameState newState)
    // {
    //     State = newState;

    //     switch (newState)
    //     {
    //         case GameState.ReadingJournal:
    //             break;
    //         case GameState.Exploration:
    //             HandleExploration();
    //             break;
    //         case GameState.BattleSystem:
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    //     }

    //     OnGameStateChanged?.Invoke(newState);
    // }

    // private void HandleExploration()
    // {

    // }


}

// public enum GameState
// {
//     ReadingJournal,
//     Exploration,
//     BattleSystem,
// }
