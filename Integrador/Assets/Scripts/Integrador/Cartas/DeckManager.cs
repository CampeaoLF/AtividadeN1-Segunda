using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.Collections;


public class DeckManager : MonoBehaviour
{
    [Header("Var")]
    
    public int tentativas = 10;
    public TextMeshProUGUI tentativasText;
    public int spriteIndex = 0;
    public CardMov currentCard;
    




    [Header("Derrota")]
    public GameObject derrotaUI;
    private bool perdeu;
    

    [Header("UI")]
  
    public BarraScore barra;
    private float score = 0;

   

    public GameObject cardPrefab;
    public Transform cardPos;
    public Sprite[] cardSprites;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        SpawnNextCard();
    }

    private void Start()
    {
        RefreshUI();
        
        
    }

    private void Update()
    {
        if (perdeu == true)
        {
            Destroy(currentCard);
        }
    }

    private void OnDisable()
    {

    }

    private void OnDestroy()
    {
        if (currentCard) currentCard.OnSwipeReleased -= OnCardReleased;
    }

    void RefreshUI()
    {
        
        if (tentativasText) tentativasText.text = tentativas.ToString();
    }

    

    void SpawnNextCard()
    {
        var position = cardPos ? cardPos.position : Vector3.zero;  
        var rotation = cardPos ? cardPos.rotation : Quaternion.identity;

        var prefab = cardSprites[UnityEngine.Random.Range(0, cardSprites.Length)];

        var go =Instantiate(cardPrefab, position, rotation);


        if (cardSprites != null && cardSprites.Length > 0) 
        {
            var spriteRender = go.GetComponent<SpriteRenderer>() ?? go.GetComponentInChildren<SpriteRenderer>();
            if(spriteRender) 
            {
              spriteRender.sprite = cardSprites[spriteIndex];
                spriteIndex = (spriteIndex + 1) % cardSprites.Length;
            }
        }

        currentCard = go.GetComponent<CardMov>();
        if (currentCard) currentCard.OnSwipeReleased += OnCardReleased;



    }

    void OnCardReleased(SwipeDecision decision)
    {
        if ( decision == SwipeDecision.None) return;
        HandleDecision(decision);

    }

    private void HandleDecision(SwipeDecision decision)
    {
        if (decision == SwipeDecision.Jogada)
        {
            if (currentCard == true)
            {
                score += 0.10f;
                barra.AlternarScore(score);
            }
            Destroy(currentCard.gameObject, 1f);
            SpawnNextCard();
            RefreshUI();
            if (score > 1)
            {
                SceneManager.LoadScene("Vitoria");
            }
        }

        if (decision == SwipeDecision.Mão)
        {
            tentativas--;
           
            RefreshUI();

            if (tentativas <= 0 && !perdeu)
            {

                derrotaUI.SetActive(true);
                
                perdeu = true;
                
            }
        }
        
        
    }

   

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
