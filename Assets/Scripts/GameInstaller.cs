using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    public override void InstallBindings() {
        SignalBusInstaller.Install(Container);
        DeclareSignals();
    }

    private void DeclareSignals() {
        Container.DeclareSignal<MovePawnSignal>();
        Container.DeclareSignal<SelectedPawnSignal>();
    }
}
