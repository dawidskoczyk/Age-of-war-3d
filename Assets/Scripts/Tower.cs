using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class Tower : MonoBehaviour
{
    [SerializeField]
    public TowerData towerData;
    [SerializeField]
    private float _towerRange;
    [SerializeField]
    private float _towerDmg;
    private float _goldToDrop;
    private float _goldToBuy;
    [SerializeField] private GameObject _target;
    
    [SerializeField] private string _tagToHit = "enemy";
    [SerializeField] private Vector3 _offsetTowerVector = new Vector3(-5, 8, 0);
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _fireRate;
    private SoundManager soundManager;
    private bool _canFire = true;
    public float TowerDmg
    {
        get { return _towerDmg; }
        set { _towerDmg = value; }
    }
    public float range
    {
        get{ return _towerRange; }
        set{ _towerRange = value; }
    }
    public string tagToHit
    {
        get { return _tagToHit; }
        set { _tagToHit = value; }
    }
    public virtual void Start()
    {
        _towerRange = towerData.attackRange;
        _towerDmg = towerData.dmg;
        _fireRate = towerData.attackSpeed;
        _goldToBuy = towerData.goldToBuy;
        _goldToDrop = towerData.goldToDrop;
        InvokeRepeating("ScanForTargets", 0f, 0.3f);
        soundManager = GameObject.Find("Audio").GetComponent<SoundManager>();
    }

    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position - _offsetTowerVector, Vector3.up, _towerRange);

        if (_target != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(_firePoint.transform.position, _target.transform.position);
        }
#endif
    }
    private void ScanForTargets()
    {
        // Find all colliders in the turret's range
        Collider[] colliders = Physics.OverlapSphere(transform.position - _offsetTowerVector, _towerRange);
        // Track the closest enemy
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(_tagToHit))
            {
                // Calculate the distance to the enemy
                float distanceToEnemy = Vector3.Distance(collider.transform.position, transform.position);

                // If the enemy is closer and hasn't been targeted, update the closest enemy
                if (distanceToEnemy < shortestDistance)
                {
                    closestEnemy = collider.gameObject;
                    shortestDistance = distanceToEnemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            _target = closestEnemy;
            ShootBullets();
        }
        else
        {
            _target = null;
        }
    }
    void ShootBullets()
    {
        if (!_canFire) return;
        _canFire = false;
        soundManager.cannon.Play();
        GameObject bulletGO = Instantiate(_bulletPrefab, _firePoint.transform.position, _firePoint.transform.rotation);
        BulletTower bullet = bulletGO.GetComponent<BulletTower>();
        bullet.BulletDmg = _towerDmg;
        bullet?.SeekTarget(_target);
        Invoke(nameof(SetCanFireTrue), _fireRate);

    }
    void SetCanFireTrue()
    {
        _canFire = true;
    }
}
