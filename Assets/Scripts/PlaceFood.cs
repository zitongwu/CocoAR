/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaceFood : MonoBehaviour
{
    public GameObject food;
    
    private ARRaycastManager raycastManager;
    private GameController gameController;
    private AudioSource audioSource;

    void Start()
    {
        raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // the commented line is for testing on PC
        // if (Input.GetMouseButtonDown(0) && gameController.IsServingFood())
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && gameController.IsServingFood()) 
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
            if (hits.Count > 0) {
                food.transform.position = hits[0].pose.position;
                audioSource.PlayOneShot(audioSource.clip, 1f);
                GameObject.Instantiate(food, hits[0].pose.position, food.transform.rotation);
            } 
        }
    }
}

