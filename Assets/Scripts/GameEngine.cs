using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {
    
    [Header("Path")] 
    [SerializeField] public List<Square> RedPath;
    [SerializeField] public List<Square> BluePath;
    [SerializeField] public List<Square> GreenPath;
    [SerializeField] public List<Square> YellowPath;

    [Header("Pawns")]
    [SerializeField] public Pawn[] RedPawns;
    [SerializeField] public Pawn[] BluePawns;
    [SerializeField] public Pawn[] GreenPawns;
    [SerializeField] public Pawn[] YellowPawns;
}
