/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasketInner : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    private GameController GameController;
    private AudioSource[] Audios;
    private List<GameObject> CurrentColliders = new List<GameObject>();
    private int Num_Green;
    private int Num_Brown;
    private int Num_Yellow;

    void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Audios = GetComponents<AudioSource>();
    }

    /// update coco count and play sound on trigger
    void OnTriggerEnter(Collider other)  
    {
        if (GameController.IsGamePlaying() && !CurrentColliders.Contains(other.gameObject)) 
        {
            if (other.gameObject.tag == "GreenCoco")
            {
                Audios[0].PlayOneShot(Audios[0].clip, 1f);
                Num_Green ++;
            }
            else if (other.gameObject.tag == "YellowCoco")
            {
                Audios[1].PlayOneShot(Audios[1].clip, 1f);
                Num_Yellow ++;
            }
            else if (other.gameObject.tag == "BrownCoco")
            {
                Audios[2].PlayOneShot(Audios[2].clip, 1f);
                Num_Brown ++;
            }
            scoreUI.text = GetTotalScore().ToString();  
            CurrentColliders.Add(other.gameObject);
        }   
    }

    void OnTriggerExit (Collider other) {

        CurrentColliders.Remove(other.gameObject);
    }

    /// get all cocos in the basket
    public List<GameObject> GetCurrentColliders()
    {
        return CurrentColliders;
    }

    public int GetTotalScore()
    {
        return Num_Green * 3 + Num_Yellow - Num_Brown * 2;
    }
}
