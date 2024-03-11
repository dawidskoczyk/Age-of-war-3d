using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenSpawn;
    [SerializeField]
    private Transform EnemyBase;
    [SerializeField]
    private Vector3 SpawnVector;
    [SerializeField]
    private GameObject[] prefab;
    [SerializeField]
    private GameObject[] prefabEvoEnemy;
    public GameObject[] prefabEvoTowerEnemy;
    int j = 0;
    [SerializeField]
    private int _spawnsToEvolve;
    private int _counter=0;
    private EvoManager evoManager;
    public GameObject[] spawnCharacters;
    private void Start()
    {
        spawnCharacters = new GameObject[3];
        prefabEvoTowerEnemy = new GameObject[3];
        ChangeSpawnCharactersInArray(prefab);
        evoManager = GameObject.Find("GameManagers").GetComponent<EvoManager>();
        SpawnCharacter(j);
        
    }
    public void SpawnCharacter(int i)
    {
        // dodaæ liczby losowe wybierania prefabów   
        if (_counter > _spawnsToEvolve) {
            EvolveAfterAmountOfSpawns();
        }      

        GameObject prefabToSpawn = spawnCharacters[i];

        StartCoroutine(waitForSpawn(prefabToSpawn));
    }
    private IEnumerator waitForSpawn(GameObject prefabToSpawn) {
       GameObject go = Instantiate(prefabToSpawn, EnemyBase.transform.position+ new Vector3(-8,-5,0), transform.rotation * Quaternion.Euler(0f, -90f, 0f));
        go.GetComponent<Character>().vectorToMove = Vector3.left;
        go.tag = "enemy";
        go.layer = LayerMask.NameToLayer("enemy");
        go.GetComponent<Character>().tagToHit = "Player";
        go.GetComponent<Character>().layerToIgnore = "enemy";
        _counter++;
        yield return new WaitForSeconds(timeBetweenSpawn);
        j = j += 1;
        if (j == 3)
            j = 0;
        SpawnCharacter(j);
    }
    private void EvolveAfterAmountOfSpawns()
    {
        ChangeSpawnCharactersInArray(prefabEvoEnemy);
        evoManager.chooseEvolution(1);
    }
    private void ChangeSpawnCharactersInArray(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++) {
            spawnCharacters[i] = array[i];
            Debug.Log(spawnCharacters[i]);
        }
            
    }
}
