using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletTower : MonoBehaviour
{
    [SerializeField] float _speed = 60f;
    private GameObject _target;
    [SerializeField] GameObject _bulletImpactPrefab;
    [SerializeField] private string _tagToHit = "enemy";
    private float _bulletDmg;
    public string TagToHit
    {
        get { return _tagToHit; }
        set { _tagToHit = value; }
    }
    public float BulletDmg
    {
        get { return _bulletDmg; }
        set { _bulletDmg = value; }
    }


    public virtual void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * _speed, Space.World);
    }
    public void SeekTarget(GameObject target)
    {
        _target = target;
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag != _tagToHit) return;

            
        //Destroy(other.gameObject);
        other.GetComponent<Character>().TakeDmg(_bulletDmg);
        GameObject bulletImpactEffect = Instantiate(_bulletImpactPrefab, transform.position, Quaternion.identity);
        bulletImpactEffect.GetComponent<ParticleSystem>().Play();
        Destroy(bulletImpactEffect, 1.5f);
        Destroy(this.gameObject);

    }
}
