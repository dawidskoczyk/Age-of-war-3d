using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _gold;
    private float _exp;
    private string _name;
    public event Action updateGui;
    [SerializeField]
    private TowerSlotAdder _slotAdder;
    private EvoManager _evo;

    public float Gold{
        get { return _gold; }
        set { _gold = value; }
    }
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public float Exp
    {
        get { return _exp; }
        set { _exp = value; }
    }
    private void Start()
    {
        Gold = 1000;
        Exp = 0;
        _slotAdder =_slotAdder.GetComponent<TowerSlotAdder>();
        _slotAdder.UpdateTowerSlotGold += DecreaseGold;
        _evo = GetComponent<EvoManager>();
        _evo.decreaseXP += DecreaseExp;
    }
    public void updateGoldValue()
    {
        updateGui?.Invoke();
    }
    public  bool DecreaseGold(GameObject obj)
    {
        if (obj != null)
        {
            Character characterComponent = obj?.GetComponent<Character>();
            Tower towerComponent = obj?.GetComponent<Tower>();
            TowerSlotAdder adderComponent = obj?.GetComponent<TowerSlotAdder>();
            if (characterComponent != null && _gold >= characterComponent.data?.goldToBuy)
            {
                _gold -= characterComponent.data.goldToBuy;
            }
            else if (towerComponent != null && _gold >= towerComponent.towerData?.goldToBuy)
            {
                _gold -= towerComponent.towerData.goldToBuy;
            }
            else if (adderComponent != null && _gold >= adderComponent.goldToBuy)
            {
                _gold -= adderComponent.goldToBuy;
            }
            else
            {
                return false;
            }

            updateGoldValue();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IncreaseGold(GameObject obj)
    {
        if (obj?.GetComponent<Character>())
        {
            _gold += obj.GetComponent<Character>().data.goldToDrop;
            _exp += obj.GetComponent<Character>().data.expToDrop;
        }
        if (obj?.GetComponent<Tower>())
        {
            _gold += obj.GetComponent<Tower>().towerData.goldToDrop;
        }

        updateGoldValue();
        return true;
    }
    public bool DecreaseExp(float value)
    {
        if (Exp < value) return false;
        else
        {
            Exp = Exp - value;
            updateGoldValue();
            return true;
        }
    }
}
