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



    public GameObject[] cardPrefab;
    public Transform[] cardPos;
    public Sprite[] cardSprites;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        for (int index = 0; index < cardPrefab.Length; index++)
        {
        SpawnNextCard(index);
        }
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

    

    void SpawnNextCard(int index)
    {
        var position = cardPos[index] ? cardPos[index].position : Vector3.zero;  
        var rotation = cardPos[index] ? cardPos[index].rotation : Quaternion.identity;

        var prefab = cardSprites[UnityEngine.Random.Range(0, cardSprites.Length)];

        var go = Instantiate(cardPrefab[index], position, rotation);


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

        var index = 0; // tem que receber o index da carta
        HandleDecision(decision, index);

    }

    private void HandleDecision(SwipeDecision decision, int index)
    {
        if (decision == SwipeDecision.Jogada)
        {
            if (currentCard == true)
            {
                score += 0.10f;
                barra.AlternarScore(score);
            }
            Destroy(currentCard.gameObject, 1f);
            SpawnNextCard(index);
            RefreshUI();
            if (score > 1)
            {
                SceneManager.LoadScene("Vitoria");
            }
        }

        if (decision == SwipeDecision.M�o)
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
