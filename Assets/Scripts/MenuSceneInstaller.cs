using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller {

    public ConnectToServer server;

    public override void InstallBindings() {
        SignalBusInstaller.Install(Container);
        BindIntances();
        DeclareSignals();
    }

    private void BindIntances() {
        Container.BindInstance(server).AsSingle();
    }

    private void DeclareSignals() {
        Container.DeclareSignal<LobbyJoinSignal>();
    }
}
