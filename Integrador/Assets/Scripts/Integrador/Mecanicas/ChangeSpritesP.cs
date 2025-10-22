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
        if (player score += 0.15f)
        {
            if(score == )
        }
    }
}
