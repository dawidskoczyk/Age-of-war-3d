using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Slash,
    MagicSpell,
    HardHit,
    Fist
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _slash;
    [SerializeField] private AudioSource _magicSpell;
    [SerializeField] private AudioSource _hardHit;
    [SerializeField] private AudioSource _fist;
    [SerializeField] private AudioSource _cannon;
    private SoundType soundType;

    public AudioSource cannon
    {
        get { return _cannon; }
        set { cannon = value; }
       
    }


    public  void PlaySoundAfterGettingHit(SoundType type)
    {
        var result = soundType switch
        {
            SoundType.Slash => _slash,
            SoundType.Fist => _fist,
            SoundType.MagicSpell => _magicSpell,
            SoundType.HardHit => _hardHit,
            _ => throw new System.NotImplementedException(),
        };
        result?.Play();
    }

   
    
}