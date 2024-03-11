using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/TowerData")]
public class TowerData : ScriptableObject
{
    [Header("Parameters")]
    [Space(10)]
    [Header("DMG")]
    [SerializeField]
    [Range(1, 500)]
    public float dmg;
    [SerializeField]
    [Range(10, 500)]
    public float attackRange;
    [SerializeField]
    public float attackSpeed;
    [Space(10)]
    [Header("Gold and Exp")]
    [SerializeField]
    public float goldToDrop;
    [SerializeField]
    public float goldToBuy;
 //   [SerializeField]
 //   public float expToBuy;
    // [SerializeField]
    //  public Sprite imageParent;

}
