using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTowerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _towerParent;
    [SerializeField]
    private Transform[] _towerSlots;
    public static AddTower[] _activeTowerSlotsScripts;
    [SerializeField]
    public GameObject[] towers;
    void Start()
    {

        

        //for (int i = 0; i < _activeTowerSlotsScripts.Length; i++)
        //{
        //    _activeTowerSlotsScripts[i];
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddTowerToActiveSlot(int i)
    {
        _activeTowerSlotsScripts = _towerParent.transform.GetComponentsInChildren<AddTower>();
        AddTower.towerType = i;
        foreach (AddTower slot in _activeTowerSlotsScripts)
        {
            if (slot.transform.GetChild(0).gameObject.activeSelf)
            {
                slot.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (slot.isFreeSlot)
            {
                slot.transform.GetChild(0).gameObject.SetActive(true);  
            }
        }
    }
    public void DeleteTowerFromSlot()
    {
        _activeTowerSlotsScripts = _towerParent.transform.GetComponentsInChildren<AddTower>();
        foreach (AddTower slot in _activeTowerSlotsScripts)
        {
            if (slot.transform.GetChild(1).gameObject.activeSelf)
            {
                slot.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if (!slot.isFreeSlot)
            {
                slot.transform.GetChild(1).gameObject.SetActive(true);
            }
                
        }
    }
    private int CountActiveChildrenObjects(Transform[] slots)
    {
        int _count = 0;
        foreach(Transform  slot in slots)
        {
            if (slot.gameObject.activeSelf)
                _count++;
        }
        Debug.Log(_count);
        return _count;
    }
}
