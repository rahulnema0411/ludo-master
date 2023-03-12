using System.Collections;
using UnityEngine;
using Zenject;

public class Dice : MonoBehaviour {
    
    [SerializeField] public Sprite[] diceFaces;
    [SerializeField] public SpriteRenderer dice, shadow;
    [SerializeField] public BoxCollider2D diceCollider;
    [SerializeField] public GameObject arrow;
    [SerializeField] public TextMesh playerPoints;

    public string diceID;

    private SignalBus _signalBus;
    private LudoBoard _ludoBoard;

    [Inject]
    public void Construct(SignalBus signalBus, LudoBoard ludoBoard) {
        _signalBus = signalBus;
        _ludoBoard = ludoBoard;
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
        if(_ludoBoard.isMultiplayer) {
            if (signal.color.ToLower().Equals(diceID.ToLower()) && signal.color.ToLower().Equals(_ludoBoard.userColor)) {
                diceCollider.enabled = true;
                if(gameObject.activeInHierarchy) StartCoroutine(ShadownFadeIn());
            } else {
                diceCollider.enabled = false;
                if(gameObject.activeInHierarchy) StartCoroutine(ShadownFadeOut());
            }
        } else {
            if(signal.color.ToLower().Equals(diceID.ToLower())) {
                diceCollider.enabled = true;
                if(gameObject.activeInHierarchy) StartCoroutine(ShadownFadeIn());
            } else {
                diceCollider.enabled = false;
                if(gameObject.activeInHierarchy) StartCoroutine(ShadownFadeOut());
            }
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

    private IEnumerator ShadownFadeOut() {
        while(shadow.color.a > 0f) {
            Color color = shadow.color;
            color.a = color.a - 0.02f;
            shadow.color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator ShadownFadeIn() {
        while(shadow.color.a < 0.3f) {
            Color color = shadow.color;
            color.a = color.a + 0.02f;
            shadow.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void UpdatePlayerPoints(int initial, int final) {
        StartCoroutine(UpdateTextValue(initial, final));
    }

    private IEnumerator UpdateTextValue(int initial, int final) {
        int value = initial;
        while (value < final) {
            yield return new WaitForSeconds(0.1f);
            value++;
            playerPoints.text = value.ToString();
        }
    }
}
