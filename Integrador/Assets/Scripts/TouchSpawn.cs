using NUnit.Framework.Constraints;
using UnityEngine;

public class TouchSpawn : MonoBehaviour
{

    public GameObject[] objetcs;
    private int index = 0;

    private AudioSource audioSource;
    public AudioClip clip; //Som que vai tocar ao remover um obj
    void Start()
    {
        AudioSource audioSource= GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == UnityEngine.TouchPhase.Began)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(touch.position);

                Collider2D hit = Physics2D.OverlapPoint(position);

                if (hit != null)
                {
                    //Tocar som
                    if (clip != null)
                    {
                        audioSource.PlayOneShot(clip);
                    }

                    Destroy(hit.gameObject);
                }

                Instantiate(objetcs[index], position, Quaternion.identity);

                index = (index + 1) % objetcs.Length;
            }
        }
    }
}
