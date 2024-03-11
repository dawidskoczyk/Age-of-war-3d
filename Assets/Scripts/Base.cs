using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [Header("HP")]
    [SerializeField]
    private float hpMax;
    [SerializeField]
    private float hpNow;
    [SerializeField]
    private Image image;
    [Header("End Screens")]
    [SerializeField]
    private GameObject _winScreen;
    [SerializeField]
    private GameObject _loseScreen;

    public void TakeDmg(float amount)
    {
        if (hpNow > 0)
        {
            hpNow = hpNow - amount;
            hpNow = Mathf.Clamp(hpNow, 0, hpMax);
            image.fillAmount = hpNow / hpMax;
            if (hpNow <= 0)
            {
                if (gameObject.layer == LayerMask.NameToLayer("alies")) _loseScreen.SetActive(true);
                    else if (gameObject.layer == LayerMask.NameToLayer("enemy")) _winScreen.SetActive(true);

                Time.timeScale = 0;
                //end game
            }
        }
    }
}
