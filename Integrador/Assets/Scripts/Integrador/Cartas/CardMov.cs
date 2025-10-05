using UnityEngine;
using UnityEngine.EventSystems;

public enum SwipeDecision { None, Jogada, M�o }

public class CardMov : MonoBehaviour
{
    [Header("Options")]
    [Tooltip("Mant�m o deslocamento entre o ponto tocado e o centro do objeto (evita 'pulo').")]
    //Pulo =  quando voc� come�a a arrastar o objeto sem considerar a diferen�a entre a posi��o do dedo (toque na tela) e o centro do objeto
    // Testar sem keepOffset para mostrar isso

    public bool keepOffset = true;

    public Camera cam;
    public int activeFingerId = -1;
    public float screenZ;               // Profundidade do objeto em coordenadas de tela
    public Vector3 dragOffset;          // Offset entre dedo e centro do objeto
    [SerializeField] bool has2D;                  // Tem Collider2D?
    //[SerializeField] bool has3D;                  // Tem Collider 3D?

    


    // >>> EVENTO PARA O MANAGER <<<
    public System.Action<SwipeDecision> OnSwipeReleased;

    void Awake()
    {
        cam = Camera.main;
        has2D = GetComponent<Collider2D>() != null;

        //has3D = GetComponent<Collider>() != null;
    }

    void OnEnable()
    {
        // Salva a profundidade atual do objeto para converter Screen->World corretamente
        screenZ = cam.WorldToScreenPoint(transform.position).z;
    }

    void Update()
    {
        if (Input.touchCount == 0) return;

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch toutch = Input.GetTouch(i);

            /*if (EventSystem.current && EventSystem.current.IsPointerOverGameObject(toutch.fingerId))
                continue; // toque est� em UI; n�o arrastar*/

            // IN�CIO DO ARRASTE: s� inicia se o toque come�ou sobre ESTE objeto
            if (toutch.phase == UnityEngine.TouchPhase.Began && activeFingerId == -1 && TouchHitsThis(toutch.position))
            {
                activeFingerId = toutch.fingerId;

                Vector3 worldAtFinger = ScreenToWorld(toutch.position);
                dragOffset = keepOffset ? (transform.position - worldAtFinger) : Vector3.zero;
            }

            // ARRASTE
            if (toutch.fingerId == activeFingerId && (toutch.phase == UnityEngine.TouchPhase.Moved || toutch.phase == UnityEngine.TouchPhase.Stationary))
            {
                Vector3 worldAtFinger = ScreenToWorld(toutch.position);
                transform.position = worldAtFinger + dragOffset;
            }

            // FIM DO ARRASTE: objeto permanece onde o dedo parou
            if (toutch.fingerId == activeFingerId && (toutch.phase == UnityEngine.TouchPhase.Ended || toutch.phase == UnityEngine.TouchPhase.Canceled))
            {

                /*Emite o evento da onde foi solto*/

                SwipeDecision decision =
                      transform.position.y > 0f ? SwipeDecision.Jogada
                    : transform.position.y < 0f ? SwipeDecision.M�o
                    : SwipeDecision.None;

                OnSwipeReleased?.Invoke(decision); // avisa o Manager

                activeFingerId = -1;
            }

        }
        
    }

    Vector3 ScreenToWorld(Vector2 screenPos)
    {
        // Converte considerando a profundidade do objeto (serve para 2D e 3D)
        var screenPosition = new Vector3(screenPos.x, screenPos.y, screenZ);
        return cam.ScreenToWorldPoint(screenPosition);
    }

    bool TouchHitsThis(Vector2 screenPos)
    {
        // Teste para 2D
        if (has2D)
        {
            Vector3 world = ScreenToWorld(screenPos);
            return Physics2D.OverlapPoint(world) == GetComponent<Collider2D>();
        }

        // Teste para 3D
        //if (has3D)
        //{
        //    Ray ray = cam.ScreenPointToRay(screenPos);
        //    return Physics.Raycast(ray, out RaycastHit hit) && hit.collider == GetComponent<Collider>();
        //}

        // Se n�o tiver collider, aceita sempre (come�a a arrastar em qualquer lugar)
        return true;
    }
}
