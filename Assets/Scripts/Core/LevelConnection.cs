using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Connections")]

public class LevelConnection : ScriptableObject
{
    public static LevelConnection ActiveConnection { get; set; }
}

//Luke Bonniwell Code