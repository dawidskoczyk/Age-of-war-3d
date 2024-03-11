using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Parameters")]
    [Space(5)]
    [Header("HP")]
    [SerializeField][Range(10,200)]
    public float hpMax;
    [SerializeField][Range(10,200)]
    public float hpNow;
    [Space(10)]
    [Header("DMG")]
    [SerializeField]
    [Range(1, 100)]
    public float dmg;
    [SerializeField]
    public float attackRange;
    [SerializeField]
    public float attackSpeed;
    [Space(10)]
    [Header("Move")]
    [SerializeField]
    public float movementSpeed;
    [SerializeField]
    public float standDistance;
    [SerializeField]
    public float spawnTime;
    [Header("Gold and Exp")]
    [SerializeField]
    public float goldToDrop;
    [SerializeField]
    public float expToDrop;
    [Space(5)]
    [SerializeField]
    public float goldToBuy;
 //   [SerializeField]
 //   public float expToBuy;
    // [SerializeField]
    //  public Sprite imageParent;

}
