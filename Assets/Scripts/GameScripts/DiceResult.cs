using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DiceResult : MonoBehaviour {

	[SerializeField] public Sprite[] diceFaces;
	[SerializeField] public SpriteRenderer dice;

    public string color;

	private SignalBus _signalBus;

	[Inject]
	public void Construct(SignalBus signalBus) {
		_signalBus = signalBus;
	}

    private void Start() {
        SubscribeToSignals();
    }

    private void SubscribeToSignals() {
		_signalBus.Subscribe<DiceResultSignal>(ShowResult);
	}

    private void ShowResult(DiceResultSignal signal) {
        if(gameObject.activeInHierarchy && signal.color.ToLower().Equals(color.ToLower())) {
            StartCoroutine(RollDice(signal.roll));
        }
    }

    private IEnumerator RollDice(int roll) {
        for (int i = 0; i < 5; i++) {
            dice.sprite = diceFaces[UnityEngine.Random.Range(0, 6)];
            yield return new WaitForSeconds(0.05f);
        }
        dice.sprite = diceFaces[roll - 1];
    }
}
