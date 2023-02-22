using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller {

    public ConnectToServer server;
    public Menu menu;

    public override void InstallBindings() {
        SignalBusInstaller.Install(Container);
        BindIntances();
        DeclareSignals();
    }

    private void BindIntances() {
        Container.BindInstance(server).AsSingle();
        Container.BindInstance(menu).AsSingle();
    }

    private void DeclareSignals() {
        Container.DeclareSignal<LobbyJoinSignal>();
        Container.DeclareSignal<GamePlayData>();
        Container.DeclareSignal<LoadSceneSignal>();
    }
}
