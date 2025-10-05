using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwipeManager : MonoBehaviour
{
    [Header("References")]
    int acceptCount, rejectCount;
    int spriteIndex = 0;
    public CardMov currentCard;

    [Header("UI")]
    public TextMeshProUGUI AcceptCountText;
    public TextMeshProUGUI RejectCountText;
    public Button AcceptButton;   // cora��o
    public Button RejectButton;   // X

    [Header("Card")]
    public GameObject cardPrefab; // prefab com SpriteRenderer + Collider(2D) + CardMover
    public Transform spawnPoint;  // onde a carta nasce
    public Sprite[] cardSprites;  // lista recebida (ordem ser� usada em sequ�ncia)

    void Start()
    {
        // bot�es fazem o mesmo efeito do swipe
        if (AcceptButton) AcceptButton.onClick.AddListener(() => HandleDecision(SwipeDecision.Jogada));
        if (RejectButton) RejectButton.onClick.AddListener(() => HandleDecision(SwipeDecision.M�o));

        RefreshUI();
        SpawnNextCard();
    }

    void OnDestroy()
    {
        if (currentCard) currentCard.OnSwipeReleased -= OnCardReleased;
    }

    // --- ciclo da carta ---
    void SpawnNextCard()
    {
        var position = spawnPoint ? spawnPoint.position : Vector3.zero;
        var rotation = spawnPoint ? spawnPoint.rotation : Quaternion.identity;

        var go = Instantiate(cardPrefab, position, rotation);

        // aplica PR�XIMO sprite da lista (sequencial, com wrap)
        if (cardSprites != null && cardSprites.Length > 0)
        {
            var spriteRender = go.GetComponent<SpriteRenderer>() ?? go.GetComponentInChildren<SpriteRenderer>();
            if (spriteRender)
            {
                spriteRender.sprite = cardSprites[spriteIndex];
                spriteIndex = (spriteIndex + 1) % cardSprites.Length;
            }
        }

        currentCard = go.GetComponent<CardMov>();
        if (currentCard) currentCard.OnSwipeReleased += OnCardReleased; // inscreve callback
    }

    void OnCardReleased(SwipeDecision decision)
    {
        if (decision == SwipeDecision.None) return; // solto em x==0 n�o conta
        HandleDecision(decision);
    }

    void HandleDecision(SwipeDecision decision)
    {
        if (decision == SwipeDecision.Jogada) acceptCount++;
        else if (decision == SwipeDecision.M�o) rejectCount++;

        RefreshUI();

        // destr�i a carta atual e gera outra
        if (currentCard)
        {
            currentCard.OnSwipeReleased -= OnCardReleased; // desinscreve callback
            Destroy(currentCard.gameObject);
            currentCard = null;
        }
        SpawnNextCard();
    }

    void RefreshUI()
    {
        if (AcceptCountText) AcceptCountText.text = acceptCount.ToString();
        if (RejectCountText) RejectCountText.text = rejectCount.ToString();
    }
}
