public class EventCode {

    //game signals
    public const byte PlayerTurnSignalEventCode = 1;
    public const byte SelectedPawnSignalEventCode = 2;
    public const byte DiceResultSignalEventCode = 3;
    public const byte MovePawnSignalEventCode = 4;
    public const byte TurnEndSignalEventCode = 5;
    public const byte KillPawnSignalEventCode = 6;

    //non in game signals
    public const byte RequestGameDataSignal = 102;
    public const byte GameDataSignal = 103;
    public const byte RoomJoinedSignal = 104;
}
