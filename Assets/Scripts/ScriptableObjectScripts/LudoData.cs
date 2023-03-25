using UnityEngine;

[CreateAssetMenu(fileName = "LudoData", menuName = "Create ScriptableObject/LudoData")]
public class LudoData : ScriptableObject
{
    public string turnOrder;
    public bool isMultiplayer;
    public bool isHost;
    public string lobbyCode;
}