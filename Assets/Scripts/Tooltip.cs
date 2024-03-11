using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private Camera _uiCamera;
    [TextArea(17, 1000)][SerializeField]
    private string _comment;
    [SerializeField]
    private int _textSize;
    private TMP_Text _tipText;
    [SerializeField] private GameObject _towersParent;
    [SerializeField] private GameObject _gameManager;
    private void Awake()
    {
        GetComponentInChildren<TMP_Text>().text = _comment;
        GetComponentInChildren<TMP_Text>().fontSize = _textSize;
    }
    private void Update()
    {
        Vector2 _localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, _uiCamera, out _localPoint);
        transform.localPosition = transform.parent.TransformPoint(_localPoint)+new Vector3(100,-100,0);
    }
    public void ShowTooltipTower(int i)
    {

        gameObject.SetActive(true);
        GetComponentInChildren<TMP_Text>().text = _comment + " cost " +_towersParent.GetComponent<AddTowerManager>().towers[i].GetComponent<Tower>().towerData.goldToBuy+" dmg "+ _towersParent.GetComponent<AddTowerManager>().towers[i].GetComponent<Tower>().towerData.dmg;
    }
    public void ShowTooltip()
    {
        gameObject.SetActive(true);
    }
    public void ShowTooltipCharacter(int i)
    {
        gameObject.SetActive(true);
        GetComponentInChildren<TMP_Text>().text = _comment + " cost " + _gameManager.GetComponent<SpawnManager>().prefab[i].GetComponent<Character>().data.goldToBuy+" dmg:" + _gameManager.GetComponent<SpawnManager>().prefab[i].GetComponent<Character>().data.dmg +" hp " + _gameManager.GetComponent<SpawnManager>().prefab[i].GetComponent<Character>().data.hpMax;
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
