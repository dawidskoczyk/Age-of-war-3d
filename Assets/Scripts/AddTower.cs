using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTower : MonoBehaviour
{
    public static int towerType;
    [Header("Tower Prefabs")]
    [Space(5)]
    [SerializeField]
    private GameObject[] towersArray;

    [Header("Parametry")]
    [Space(5)]
    [SerializeField]
    string _tagEnemy;
    [SerializeField]
    Vector3 _offsetToPlaceTower;
    private Player player;
   

    bool _isFreeSlot = true;
    private void Start()
    {
        player = GameObject.Find("GameManagers").GetComponent<Player>();
       
    }
    public bool isFreeSlot
    {
        get { return _isFreeSlot; }
        set { _isFreeSlot = value; }
    }
    public void AddTowerToEmptySlot()
    {
        if (!_isFreeSlot) return;
        towersArray = GetComponentInParent<AddTowerManager>().towers;
        GameObject go = Instantiate(towersArray[towerType], transform.position + _offsetToPlaceTower, Quaternion.identity);
        _isFreeSlot = false;
        foreach (var slot in AddTowerManager._activeTowerSlotsScripts)
            slot.transform.GetChild(0).gameObject.SetActive(false);
        if (!player.DecreaseGold(go)) {
            _isFreeSlot = true;

            Destroy(go);
            return;
            };
        Tower towerScript = go?.GetComponent<Tower>();
        towerScript.tagToHit = _tagEnemy;
        go.transform.parent = transform;

    }
    public void DeleteTowerFromSlot()
    {
        if (_isFreeSlot) return;
        GameObject go = transform.GetChild(2).gameObject;
        if (!player.IncreaseGold(go)) return;
        Destroy(go);
        isFreeSlot = true;
        foreach (var slot in AddTowerManager._activeTowerSlotsScripts)
            slot.transform.GetChild(1).gameObject.SetActive(false);
    }
    
}
