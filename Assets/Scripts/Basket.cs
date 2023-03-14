/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Basket : MonoBehaviour
{
    public EndInterface endInterface;
    private Camera cam;
    public GameController gameController;
    private Animator anim;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    void Update()
    {
        // set basket position relative to the camera
        float rotate_y = cam.transform.eulerAngles.y;
        if (!gameController.IsGamePlaying() && endInterface.GetAnimationStatus() == 1) {
            anim.enabled = true;
        }
        else {
            transform.eulerAngles = new Vector3(-90f, rotate_y, 0);
        }
    }
}
