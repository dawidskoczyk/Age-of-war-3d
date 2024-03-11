using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerSlotAdder : MonoBehaviour
{
    [SerializeField] private List<GameObject> _towerSlots;
    private int _slotsCount; //iloœæ slotów
    [SerializeField]
    private int _goldToBuy;
    [SerializeField]
    private Material _material;
    
    public int goldToBuy
    {
        get { return _goldToBuy; }
        set { _goldToBuy = value; }

    }
    public Material material
    {
        get { return _material; }
        set { _material = value; }

    }
    public delegate bool TowerSlotGoldUpdateHandler(GameObject obj);
    public event TowerSlotGoldUpdateHandler UpdateTowerSlotGold;

    void Start()
    {
        _slotsCount = 0; // na starcie jeden slot aktywny
    }

    public void AddTowerSlot()
    { 
        if (_slotsCount == _towerSlots.Count ) return; //sprawda czy nie wykorzystano ju¿ wszystkich slotów
        if(!UpdateTowerSlotGold.Invoke(gameObject)) return;
        _towerSlots[_slotsCount].GetComponent<MeshRenderer>().material = _material;
        _towerSlots[_slotsCount].SetActive(true);
        _towerSlots[_slotsCount].transform.GetChild(0).gameObject.SetActive(false);
        _slotsCount++;
    }
}
