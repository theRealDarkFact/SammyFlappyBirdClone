using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform[] SpawnLocations;
    [SerializeField] private int RandomDelayMin, RandomDelayMax;
    private int RandomMax;
    private bool Spawning = false;
    public bool StartSpawner = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        RandomMax = Random.Range(RandomDelayMin, RandomDelayMax);
    }

    private void Update()
    {
        if (StartSpawner && !Spawning) 
        {
            StartCoroutine(SpawnBird());            
        }
    }

    private IEnumerator SpawnBird() 
    {
        Spawning = true;
        yield return new WaitForSeconds(RandomMax);
        RandomMax = Random.Range(RandomDelayMin, RandomDelayMax);

        if(Random.Range(0, 1000) > 850) 
        {
            foreach(Transform st in SpawnLocations) 
            {
                Instantiate(Enemy, st.position, Quaternion.identity);
            }
        }
        else 
        {
            Instantiate(Enemy, SpawnLocations[Random.Range(0, SpawnLocations.Length)].position, Quaternion.identity);
        }
        Spawning = false;
    }
}
