using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class TextManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _goldText;
    [SerializeField]
    private TMP_Text _expText;
    [SerializeField]
    private Player _player;
    void Start()
    {
        _player = GetComponent<Player>();
        _player.updateGui += updateGui;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void updateGui()
    {
        _goldText.text = _player.Gold.ToString();
        _expText.text = _player.Exp.ToString();
    }
}
