using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller {

    public GameManager gameManager;

    public override void InstallBindings() {
        BindIntances();
    }

    private void BindIntances() {
        Container.BindInstance(gameManager).AsSingle();
    }
}
