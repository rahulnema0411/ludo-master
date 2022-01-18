using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Dice : MonoBehaviour {
    
    [SerializeField] public Sprite[] diceFaces;
    [SerializeField] public SpriteRenderer dice;

    public string diceID;

    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    private void Start() {
        SubscribeToSignals();
    }

    private void SubscribeToSignals() {
        _signalBus.Subscribe<PlayerTurnSignal>(ShowOrHideDice);
    }

    private void ShowOrHideDice(PlayerTurnSignal signal) {
        if(signal.color.ToLower().Equals(diceID.ToLower())) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }

    void Update() {
        DetectHit();
    }

    private void DetectHit() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.transform.GetComponent<Dice>() != null) {
                if (hit.transform.GetComponent<Dice>().diceID.Equals(diceID)) {
                    int roll = Random.Range(0, 6);
                    dice.sprite = diceFaces[roll];
                    _signalBus.Fire(new DiceResultSignal { 
                        roll = roll + 1,
                        color = diceID
                    });
                }
            }
        }
    }
}
