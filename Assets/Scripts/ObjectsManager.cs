/*
Manage instantiation of cocos in the scence
Author: Zitong Wu
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectsManager : MonoBehaviour
{
    public GameObject[] objs;
    public GameObject env;
    private GameObject basket;
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private GameController gameController;
    private GameObject score;
    private Camera cam;
    private AudioSource audioSouce;
    private int objectsTypeNum;

    void Awake()
    {
        score = GameObject.FindGameObjectWithTag("Score");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        basket = GameObject.Find("AR Session Origin/AR Camera/Basket");
        audioSouce = GetComponent<AudioSource>();
        raycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();
        planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
        objectsTypeNum = objs.Length;
        env.SetActive(true);
        basket.SetActive(false);
    }

    void Update()
    {
        // initialize scene and cocos on screen touch
        if (gameController.IsGamePlaying() && Input.GetMouseButtonDown(0))
        // if (gameController.IsGamePlaying() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // List<ARRaycastHit> hits = new List<ARRaycastHit>();
            // raycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);
            StartCoroutine(InitializeScene());
        }
    }

    // IEnumerator InitializeScene(List<ARRaycastHit> hits){
    //     GameObject.Instantiate(env, hits[0].pose.position - new Vector3(0f, 0.5f, 0f), env.transform.rotation);
    //     // disable planeManager to prevent further plane detection
    //     planeManager.enabled = false;
    //     yield return new WaitForSeconds(1);
    //     basket.SetActive(true);
    //     yield return new WaitForSeconds(1);
    //     score.SetActive(true);
    //     yield return new WaitForSeconds(1);
    //     float distance = 2f;
    //     Vector3 center = cam.transform.position + cam.transform.forward * distance;
    //     StartCoroutine(CreateObjects(center));
    // }

    IEnumerator InitializeScene()
    {
        float distance = 2f;
        Vector3 center = cam.transform.position + cam.transform.forward * distance;
        GameObject.Instantiate(env, center - new Vector3(0f, 0.5f, 0f), env.transform.rotation);
        planeManager.enabled = false;
        yield return new WaitForSeconds(1);
        basket.SetActive(true);
        yield return new WaitForSeconds(1);
        score.SetActive(true);
        yield return new WaitForSeconds(1);
        StartCoroutine(CreateObjects(center));
        yield return null;
    }


    IEnumerator CreateObjects(Vector3 center)
    {
        int totalObjects = 20;
        float radius = 1f;
        // instantiate cocos
        for (int i = 0; i < totalObjects; i++) {
            Vector3 pos = center + new Vector3(Random.Range(-radius, radius), Random.Range(2f, 4f), Random.Range(-radius, radius));
            GameObject.Instantiate(objs[i % objectsTypeNum] , pos, objs[i % objectsTypeNum].transform.rotation);
            audioSouce.PlayOneShot(audioSouce.clip, 0.7f);
            yield return new WaitForSeconds(Random.Range(0f, 1f));
        }
    }

}


