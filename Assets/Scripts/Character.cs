using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterState { Idle, Walk, Attack, Die }


public abstract class Character : MonoBehaviour
{

    #region variables
    [SerializeField] CharacterState state;
    [SerializeField] bool isLocked;

    [SerializeField] AnimationClip attackAnim;
    [SerializeField] GameObject bloodParticle;
    [SerializeField]
    public CharacterData data;
    protected float hpMax;
    protected float hpNow;
    protected float dmg;
    protected float attackRange;
    protected float attackSpeed;
    protected Collider hitbox;
    protected float movementSpeed;
    protected float standDistance;
    [SerializeField]
    protected Image image;
    [SerializeField]
    protected bool canAttack;
    public bool canMove;
    protected float spawnTime;

    public RaycastHit hitAlly, hitEnemy;
    public Ray rayAlly, rayEnemy;
    public Vector3 vectorToMove;
    public string layerToIgnore;
    public string tagToHit;
    public List<RaycastHit> sortedHits;
    protected RaycastHit target;

    public bool destroyed;

    public Player player;
    public float _expToDrop;
    public float _goldToDrop;
    public float _goldToBuy;
    [SerializeField]
    private GameObject _dmgIndicator;
    [SerializeField]
    private GameObject _dmgIndicatorEnemy;

    private GameObject _indicator;

    [SerializeField]
    private GameObject _bulletImpactPrefab;

    public Animator animator;
    public bool triggered = false;
    private SoundManager soundManager;
    [SerializeField] private SoundType soundType;
   
    #endregion
    [SerializeField] List<GameObject> sortedEnemies;
    public virtual void Start()
    {
        hpMax = data.hpMax;
        hpNow = data.hpNow;
        dmg = data.dmg;
        attackRange = data.attackRange;
        attackSpeed = data.attackSpeed;
        hitbox = GetComponent<CapsuleCollider>();
        movementSpeed = data.movementSpeed;
        standDistance = data.standDistance;
        spawnTime = data.spawnTime;
        _expToDrop = data.expToDrop;
        _goldToDrop = data.goldToDrop;
        _goldToBuy = data.goldToBuy;
        canMove = false;
        canAttack = false;
        destroyed = false;
        player = GameObject.Find("GameManagers").GetComponent<Player>();
        animator = GetComponent<Animator>();
        soundManager = GameObject.Find("Audio").GetComponent<SoundManager>();
        
        if (gameObject.tag == "enemy")
        {
            vectorToMove = Vector3.left;
            layerToIgnore = "enemy";
            player.Name = "Enemy";
            _indicator = _dmgIndicatorEnemy;
        }
        else
        {
            vectorToMove = Vector3.right;
            layerToIgnore = "alies";
            player.Name = "Player";
            _indicator = _dmgIndicator;
        }

        state = CharacterState.Idle;
        isLocked = false;
        
    }
    public virtual void Update()
    {
        if (isLocked) return;

        EvaluateState();

        switch (state)
        {
            case CharacterState.Idle:
                animator.SetTrigger("1");
                StopAllCoroutines();

                break;
            case CharacterState.Walk:
                StopAllCoroutines();
                Move();
                break;
            case CharacterState.Attack:
                StartCoroutine(Attack());
                break;
            case CharacterState.Die:
                StopAllCoroutines();
                OnDie();
                break;
            default:
                break;
        }
    }

    void EvaluateState()
    {
        if (state == CharacterState.Die) return;

        CheckAttack();
        CheckMove();
        if (!canAttack && !canMove) ChangeCharacterState(CharacterState.Idle);
        else if (canAttack && !canMove) ChangeCharacterState(CharacterState.Attack);
        else if (canAttack && canMove) ChangeCharacterState(CharacterState.Attack);
        else if (!canAttack && canMove) ChangeCharacterState(CharacterState.Walk);
    }

    void Move()
    {
        animator.Play("Walk");
        transform.position += vectorToMove * Time.deltaTime * data.movementSpeed;
    }
    public virtual void OnDie()
    {
        isLocked = true;
        animator.SetTrigger("7");
        if (tagToHit == "Player")
            player.IncreaseGold(this.gameObject);

        Destroy(gameObject, 1f);
    }
    public virtual void TakeDmg(float amount)
    {
        animator.SetTrigger("6");
        hpNow = hpNow - amount;
        hpNow = Mathf.Clamp(hpNow, 0, hpMax);
        image.fillAmount = hpNow / hpMax;
        DmgIndicator indicator = Instantiate(_indicator, transform.position, Quaternion.identity).GetComponent<DmgIndicator>();
        indicator.SetText(amount);
        GameObject bp = Instantiate(bloodParticle, transform.position, Quaternion.identity);
        
        PlayParticleAfterBeingHit();
        
        if (hpNow <= 0)
        {
            ChangeCharacterState(CharacterState.Die);
        }
        Destroy(bp, 1f);
    }

    public bool CheckMove()
    {
        rayAlly = new Ray(transform.position + new Vector3(0f, 1.5f, 0f), vectorToMove);
        Physics.Raycast(rayAlly, out hitAlly);

        canMove = (hitAlly.distance <= standDistance) ? false : true;

        return canMove;
    }

    public bool CheckAttack()
    {
        rayEnemy = new Ray(transform.position + new Vector3(0f, 1.5f, 0f), vectorToMove);
        int layerMask = ~LayerMask.GetMask(layerToIgnore);
        RaycastHit[] hits;

        hits = Physics.RaycastAll(rayEnemy, attackRange, layerMask);

        sortedHits = SortHitsByIncreasingDistance(hits);

        sortedEnemies.Clear();
        foreach (var item in sortedHits)
        {
            sortedEnemies.Add(item.collider.gameObject);
        }

        canAttack = (sortedHits.Count > 0) ? true : false;

        return canAttack;
    }

    public void PlayParticleAfterBeingHit()
    {
        GameObject bulletImpactEffect = Instantiate(_bulletImpactPrefab, transform.position, Quaternion.identity);
        bulletImpactEffect.GetComponent<ParticleSystem>().Play();
        Destroy(bulletImpactEffect, 1.5f);
    }
    private List<RaycastHit> SortHitsByIncreasingDistance(RaycastHit[] hits)
    {
        List<RaycastHit> sortedHits = new List<RaycastHit>(hits);
        sortedHits?.Sort((x, y) => x.distance.CompareTo(y.distance));
        return sortedHits;
    }

    public IEnumerator Attack()
    {
        isLocked = true;


        if (sortedHits.Count > 0)
        {
            if (sortedHits[0].collider.tag == tagToHit || sortedHits[0].collider.tag == "base")
            { 
                animator.SetTrigger("4");
                DealDmg(sortedHits[0]);
                yield return new WaitForSeconds(attackAnim.length * attackSpeed);
                soundManager.PlaySoundAfterGettingHit(soundType);
                
            }
        }
        isLocked = false;

    }
    public void DealDmg(RaycastHit hit)
    {
        //animator.SetTrigger("4");
        //yield return new WaitForSeconds(attackSpeed*2);
        if(hit.collider.tag == tagToHit)
            hit.collider.GetComponent<Character>().TakeDmg(dmg);
        else
            hit.collider.GetComponent<Base>().TakeDmg(dmg);
    }

    void ChangeCharacterState(CharacterState characterState)
    {
        state = characterState;
    }

}
