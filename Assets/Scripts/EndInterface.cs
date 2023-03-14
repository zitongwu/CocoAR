/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInterface : MonoBehaviour
{
    private int animationStatus = 0;
    
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySound() 
    {
        animationStatus = 1;
        audioSource.Play();
    }

    void SetAnimationStatus()
    {
        animationStatus = 2;
    }

    public int GetAnimationStatus() 
    {
        return animationStatus;
    }
}
