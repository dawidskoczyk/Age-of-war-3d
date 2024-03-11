using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EvoManager : MonoBehaviour
{
    #region variable
    [SerializeField]
    private GameObject[] _prefabCharactersPrehistoric;
    [SerializeField]
    private GameObject[] _prefabTowersPrehistoric;
    [SerializeField]
    private GameObject[] _prefabCharactersMedieval;
    [SerializeField]
    private GameObject[] _prefabTowersMedieval;
    private GameObject[] _spawnManagerPrefabs;
    private GameObject[] _spawnManagerPrefabsEnemy;
    private GameObject[] _addTowerPrefabs;
    private GameObject[] _addTowerPrefabsEnemy;
    [SerializeField]
    private MeshRenderer[] _towerSlots;
    [SerializeField]
    private MeshRenderer[] _towerSlotsEnemy;
    [SerializeField]
    private Material _material;
    [SerializeField]
    private GameObject[] _evolveTowers;
    [SerializeField]
    private GameObject[] _evolveTowersEnemy;
    [SerializeField] private GameObject _towersParent;
    [SerializeField] private GameObject _towersParentEnemy;
    [SerializeField] private float expToEvo = 6000;
    public delegate bool EvoUpdateHandler(float value);
    public event EvoUpdateHandler decreaseXP;
    #endregion

    void Start()
    {
        _spawnManagerPrefabs = GetComponent<SpawnManager>().prefab;
        _spawnManagerPrefabsEnemy = GetComponent<EnemySpawner>().spawnCharacters;
        _addTowerPrefabs = _towersParent.GetComponent<AddTowerManager>().towers;
        _addTowerPrefabsEnemy = GetComponent<EnemySpawner>().prefabEvoTowerEnemy;
        _towerSlotsEnemy = _towersParentEnemy.transform.GetComponentsInChildren<MeshRenderer>();
    }



//w tablicy
//0 -> soldier
//1 -> archer
//2 -> tank
    public void chooseEvolution(int i)
    {
        _towerSlots = _towersParent.transform.GetComponentsInChildren<MeshRenderer>();
        if (i == 1) {
            evolve(_spawnManagerPrefabsEnemy, _addTowerPrefabsEnemy, _prefabCharactersMedieval, _prefabTowersMedieval, _towerSlotsEnemy, _towersParentEnemy, _evolveTowersEnemy);//ewolucja przeciwnika
            return;
        }
        if (!decreaseXP(expToEvo)) return;
        if (i == 0) evolve(_spawnManagerPrefabs, _addTowerPrefabs,_prefabCharactersMedieval, _prefabTowersMedieval,_towerSlots, _towersParent, _evolveTowers);//ewolucja gracza
    }
    public void evolve(GameObject[] spawnManagerPrefabs, GameObject[] addTowerPrefabs, GameObject[] evoArrayCharacter, GameObject[] evoArrayTower, MeshRenderer[] towerSlots,GameObject towersParent,GameObject[] evolveTowers)
    {
        for(int i = 0; i < spawnManagerPrefabs.Length; i++)
        {
            spawnManagerPrefabs[i] = evoArrayCharacter[i];
        }
        for (int i = 0; i < _addTowerPrefabs.Length; i++)
        {
            addTowerPrefabs[i] = evoArrayTower[i];
        }
        foreach (var item in towerSlots) item.material = _material;
        towersParent.GetComponent<TowerSlotAdder>().material = _material;
        evolveTowers[0].SetActive(false);
        evolveTowers[1].SetActive(true);

    }
}
