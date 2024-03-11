using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgIndicator : MonoBehaviour
{
    [SerializeField] private TMP_Text _dmgPopUp;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _minDist;
    [SerializeField] private float _maxDist;
    private float timer;
    private Vector3 _initPos;
    private Vector3 _targetPos;
    private float fraction;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        float direction = Random.rotation.eulerAngles.z;
        _initPos = transform.position;
        float Dist = Random.Range(_minDist, _maxDist);
        _targetPos = _initPos + (Quaternion.Euler(0, 0, direction) * new Vector3(Dist, Dist, 0));
        fraction = _lifeTime / 2f;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
       
        if (timer > _lifeTime) Destroy(gameObject);
        else if(timer > fraction){
            _dmgPopUp.color = Color.Lerp(_dmgPopUp.color, Color.clear, (timer - fraction) / (_lifeTime - fraction));
        }

        transform.localPosition = Vector3.Lerp(_initPos, _targetPos, Mathf.Sin(timer / _lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / _lifeTime));
    }
    public void SetText(float dmg)
    {

        _dmgPopUp.text = dmg.ToString();
    } 

}
