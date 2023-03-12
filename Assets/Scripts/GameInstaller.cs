using Zenject;

public class GameInstaller : MonoInstaller {

    public LudoBoard ludoBoard;
    public SendEventMultiplayer sendEventMultiplayer;
    public ReceiveEventMultiplayer receiveEventMultiplayer;
    public GameEngine gameEngine;

    public override void InstallBindings() {
        SignalBusInstaller.Install(Container);
        BindIntances();
        DeclareSignals();
    }

    private void DeclareSignals() {
        Container.DeclareSignal<MovePawnSignal>();
        Container.DeclareSignal<SelectedPawnSignal>();
        Container.DeclareSignal<DiceResultSignal>();
        Container.DeclareSignal<PlayerTurnSignal>();
        Container.DeclareSignal<TurnEndSignal>();
        Container.DeclareSignal<KillPawnSignal>();
        Container.DeclareSignal<UpdatePlayerPoints>();
    }

    private void BindIntances() {
        Container.BindInstance(ludoBoard).AsSingle();
        Container.BindInstance(sendEventMultiplayer).AsSingle();
        Container.BindInstance(receiveEventMultiplayer).AsSingle();
        Container.BindInstance(gameEngine).AsSingle();
    }
}
