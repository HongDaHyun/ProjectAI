using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource bgm;
    public AudioSource[] sfx;
    public AudioClip[] sfxClip;
    int sfxCursor;
    public enum SFX { Fire, CardOpen, SelectCard, DropSlot, Reload }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    public void SFXPlayer(SFX type)
    {
        switch(type)
        {
            case SFX.Fire:
                sfx[sfxCursor].clip = sfxClip[Random.Range(0, 5)];
                break;

            case SFX.CardOpen:
                sfx[sfxCursor].clip = sfxClip[5];
                break;
            case SFX.SelectCard:
                sfx[sfxCursor].clip = sfxClip[Random.Range(6, 8)];
                break;
            case SFX.DropSlot:
                sfx[sfxCursor].clip = sfxClip[Random.Range(8, 14)];
                break;

            case SFX.Reload:
                sfx[sfxCursor].clip = sfxClip[14];
                break;
        }
        if(sfx[sfxCursor].isPlaying)
            sfxCursor = (sfxCursor + 1) & sfx.Length;
        sfx[sfxCursor].Play();
        sfxCursor = (sfxCursor + 1) & sfx.Length;
    }
}
