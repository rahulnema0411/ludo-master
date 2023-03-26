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

    
    private static readonly float[] sliderValues = { 0.2f, 0.48f, 0.74f, 1f };
    private const float sliderDOMoveDuration = 0.05f;
    public LudoData data;

    private ConnectToServer _server; 
    private SignalBus _signalBus;
    private MainMenu _mainMenu;

    [Inject]
    public void Construct(ConnectToServer server, SignalBus signalBus, MainMenu mainMenu) {
        _server = server;
        _signalBus = signalBus;
        _mainMenu = mainMenu;
    }

    private void Start() {
        SetButtons();
        SetLocalLudoData("red green");
    }

    private void SetButtons() {

        createLobbyButton.onClick.RemoveAllListeners();
        createLobbyButton.onClick.AddListener(delegate() { 
            string lobbyCode = GenerateRandomLobbyCode(6);
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            data.lobbyCode = lobbyCode;
            _mainMenu.SetLudoData(data);
            _server.CreateRoom(lobbyCode, roomOptions);
        });
        
        button1P.AddOnClickListener(delegate() {
            slider.DOValue(sliderValues[0], sliderDOMoveDuration);
            SetLocalLudoData("red");
        });
        button2P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[1], sliderDOMoveDuration);
            SetLocalLudoData("red green");
        });
        button3P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[2], sliderDOMoveDuration);
            SetLocalLudoData("red green yellow");
        });
        button4P.AddOnClickListener(delegate () {
            slider.DOValue(sliderValues[3], sliderDOMoveDuration);
            SetLocalLudoData("red green yellow blue");
        });
    }

    public string GenerateRandomLobbyCode(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        System.Random random = new System.Random();
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private void SetLocalLudoData(string turnOrder) {
        data.isHost = true;
        data.isMultiplayer = true;
        data.turnOrder = turnOrder;
    }
}
