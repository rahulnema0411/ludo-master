using System.Linq;
using DG.Tweening;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CreateLobbyMenu : MonoBehaviour
{
    public BaseButton createLobbyButton;
    public BaseButton button1P, button2P, button3P, button4P;
    public Slider slider;

    public LudoData ludoData;
    
    private static readonly float[] sliderValues = { 0.2f, 0.48f, 0.74f, 1f };
    private const float sliderDOMoveDuration = 0.05f;

    private ConnectToServer _server; 
    private SignalBus _signalBus;

    [Inject]
    public void Construct(ConnectToServer server, SignalBus signalBus) {
        _server = server;
        _signalBus = signalBus;
    }

    private void Start() {
        SetButtons();
        button2P.onClick.Invoke();
    }

    private void SetButtons() {

        createLobbyButton.onClick.RemoveAllListeners();
        createLobbyButton.onClick.AddListener(delegate() { 
            string lobbyCode = GenerateRandomLobbyCode(6);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            ludoData.lobbyCode = lobbyCode;
            _server.CreateRoom(lobbyCode, roomOptions);
        });
        
        button1P.AddOnClickListener(delegate() {
            slider.DOValue(sliderValues[0], sliderDOMoveDuration);
            SetLudoData("red");
        });
        button2P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[1], sliderDOMoveDuration);
            SetLudoData("red green");
        });
        button3P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[2], sliderDOMoveDuration);
            SetLudoData("red green yellow");
        });
        button4P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[3], sliderDOMoveDuration);
            SetLudoData("red green yellow blue");
        });
    }

    public string GenerateRandomLobbyCode(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        System.Random random = new System.Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private void SetLudoData(string turnOrder) {
        ludoData.isHost = true;
        ludoData.isMultiplayer = true;
        ludoData.turnOrder = turnOrder;
    }
}
