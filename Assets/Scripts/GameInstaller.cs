using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {

    public LudoBoard ludoBoard;

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
    }

    private void BindIntances() {
        Container.BindInstance(ludoBoard).AsSingle();
    }
}
