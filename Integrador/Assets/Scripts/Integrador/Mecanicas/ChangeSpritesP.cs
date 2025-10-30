using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpritesP : MonoBehaviour
{
    public SpriteRenderer player;
    public Sprite[] sprites;
    

    [Header("Domínio")]
    [SerializeField] private DeckManager deckManager;
    

    private void OnEnable()
    {
        DeckManager.OnChangeProgressBar += OnChangeProgressBar;
    }

    private void OnDisable()
    {
        DeckManager.OnChangeProgressBar -= OnChangeProgressBar;
    }


    private void OnChangeProgressBar(float valueBar)
    {
        
        if(valueBar >= 0 && valueBar <= 0.15f)
        {
            player.sprite = sprites[0];
            
        }
        else if (valueBar >= 0.16f && valueBar <= 0.30f)
        {
            player.sprite = sprites[1];

        }
        else if (valueBar >= 0.31f && valueBar <= 0.45f)
        {
            player.sprite = sprites[2];

        }
        else if (valueBar >= 0.46f && valueBar <= 0.60f)
        {
            player.sprite = sprites[3];

        }
        else if (valueBar >= 0.61f && valueBar <= 0.76f)
        {
            player.sprite = sprites[4];

        }
        else if (valueBar >= 0.77f && valueBar <= 0.92f)
        {
            player.sprite = sprites[5];

        }
       

        // O valueBar é maior ou igual a 0.16f E o valueBar é menor ou igual a 0.30f?
        // qualquer de 0.16f a 0.30f


        //AQUI  VAI A LÓGICA DE TROCA DE SPRITE
        //EX: if(valueBar >= 0 && valueBar <= 0.25){
        //Chama o change do Sprite render ex sprites[0]

        //}
    }

    private void Start()
    {
       player = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
    }

}
