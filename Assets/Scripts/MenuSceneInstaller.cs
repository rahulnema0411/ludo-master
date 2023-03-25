using Zenject;

public class MenuSceneInstaller : MonoInstaller {

    public ConnectToServer server;
    public Menu menu;
    public MainMenu mainMenu;

    public override void InstallBindings() {
        SignalBusInstaller.Install(Container);
        BindIntances();
        DeclareSignals();
    }

    private void BindIntances() {
        Container.BindInstance(server).AsSingle();
        Container.BindInstance(menu).AsSingle();
        Container.BindInstance(mainMenu).AsSingle();
    }

    private void DeclareSignals() {
        Container.DeclareSignal<LobbyJoinSignal>();
        Container.DeclareSignal<GamePlayData>();
        Container.DeclareSignal<LoadSceneSignal>();
    }
}
