/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource[] Audios;

    void Start()
    {
        Audios = GetComponents<AudioSource>();
    }

    void PlayGreen() {
        Audios[0].PlayOneShot(Audios[0].clip);
    }

    void PlayYellow() {
        Audios[1].PlayOneShot(Audios[1].clip);
    }

    void PlayBrown() {
        Audios[2].PlayOneShot(Audios[2].clip);
    }
}
