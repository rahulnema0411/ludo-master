using UnityEngine;

[CreateAssetMenu(fileName = "LudoData", menuName = "CreateScriptableObject/LudoData")]
public class LudoData : ScriptableObject
{
    public string turnOrder;
    public bool isMultiplayer;
    public bool isHost;
    public string lobbyCode;
}