using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Zenject;
using Photon.Realtime;

public class Dice : MonoBehaviour {
    
    [SerializeField] public Sprite[] diceFaces;
    [SerializeField] public SpriteRenderer dice;
    [SerializeField] public BoxCollider2D diceCollider;
    [SerializeField] public GameObject arrow;

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
        _signalBus.Subscribe<TurnEndSignal>(HideArrow);
    }

    private void HideArrow() {
        arrow.SetActive(false);
    }

    private void ShowOrHideDice(PlayerTurnSignal signal) {
        if(signal.color.ToLower().Equals(diceID.ToLower())) {
            diceCollider.enabled = true;
            gameObject.SetActive(true);
        } else {
            diceCollider.enabled = false;
            gameObject.SetActive(false);
        }
    }

    void Update() {
        DetectHit();
        EnableArrow();
    }

    private void EnableArrow() {
        if (diceCollider.isActiveAndEnabled) {
            arrow.SetActive(true);
        } else {
            arrow.SetActive(false);
        }
    }

    private void DetectHit() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.transform.GetComponent<Dice>() != null) {
                if (hit.transform.GetComponent<Dice>().diceID.Equals(diceID)) {
                    StartCoroutine(RollDice());
                }
            }
        }
    }

    private IEnumerator RollDice() {
        diceCollider.enabled = false;
        for (int i = 0;i < 20;i++) {
            dice.sprite = diceFaces[Random.Range(0, 6)];
            yield return new WaitForSeconds(0.05f);
        }
        int roll = Random.Range(0, 6);
        dice.sprite = diceFaces[roll];
        yield return new WaitForSeconds(1f);
        _signalBus.Fire(new DiceResultSignal {
            roll = roll + 1,
            color = diceID
        });
    }
}
