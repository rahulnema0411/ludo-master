using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCode {

    //game signals
    public const byte PlayerTurnSignalEventCode = 1;
    public const byte SelectedPawnSignalEventCode = 2;
    public const byte DiceResultSignalEventCode = 3;
    public const byte MovePawnSignalEventCode = 4;
    public const byte TurnEndSignalEventCode = 5;
    public const byte KillPawnSignalEventCode = 6;

    //color selection
    public const byte ColorSelectionEventCode = 101;
    public const byte RequestTurnOrderSignal = 102;
    public const byte TurnOrderSignal = 103;
}
