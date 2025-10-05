using System.Collections;
using UnityEngine;

public class SpawnLoop : MonoBehaviour
{
    [Header("Prefabs Sorteadas")]
    public GameObject[] prefabs;

    [Header("Pontos de Spawn(fora de tela)")]
    public Transform[] spawnPoints;

    [Header("Loop - Tempo entre spawn")]
    public float spawnTime = 1.5f;

    [Header("Destino dos Objetos")]
    public Vector2 destiny = Vector2.zero;

    void Start()
    {
        StartCoroutine(LoopSpawn());
    }
    private IEnumerator LoopSpawn()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void Spawn()
    {
        if (prefabs.Length == 0 || spawnPoints.Length == 0) return;
       
        var prefab = prefabs[Random.Range(0, prefabs.Length)];
        var point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var go = Instantiate(prefab, point.position, Quaternion.identity);

        var move = go.GetComponent<SimpleMove2D>();
        if (move != null)
        {
            move = go.AddComponent<SimpleMove2D>();
            move.destiny = destiny;
        }
    }

    void Update()
    {
        
    }
}
