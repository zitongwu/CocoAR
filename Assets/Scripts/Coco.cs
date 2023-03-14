/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Coco : MonoBehaviour
{
    // to slow down the falling speed of cocos
    public float CounterGravity = 9f;
    public GameObject prefab;
    public VisualEffect SmokePoof;

    private Rigidbody rigibBody;
    private Camera cam;
    private AudioSource[] audios;
    private Renderer rend;
    private GameController gameController;
    private float whooshVolume = 0.7f;
    private float dungVolume = 0.5f;

    /// dissolve Effect parameters on object destroy
    private float DissolveSpeed = 1f;
    private bool Dissolve = false;
    private float DissolvePercent = 0f;


    void Start()
    {
        rigibBody = GetComponent<Rigidbody>();
        audios = GetComponents<AudioSource>();
        rend = GetComponent<Renderer>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        // add counter-gravity force to slow down the falling speed
        if (rigibBody.useGravity) { 
            rigibBody.AddForce(new Vector3(0, 1.0f, 0) * rigibBody.mass * CounterGravity); 
        }
    }

    void OnCollisionEnter(Collision collision)  
    {
        if (collision.gameObject.tag == "Ground")
        {
            // play smokepoof
            Vector3 pos = gameObject.transform.position;
            GameObject.Instantiate(SmokePoof, pos, SmokePoof.transform.rotation);

            // play sound
            audios[0].PlayOneShot(audios[0].clip, dungVolume);

            // destroy the object
            StartCoroutine(CustomDestroy(9));
        }
    }

    IEnumerator CustomDestroy(float second) 
    {
        // delay destroy process
        yield return new WaitForSeconds(second);

        float radius = 1f;
        float distance = 1f;
        Vector3 center = cam.transform.position + cam.transform.forward * distance;
        Vector3 pos = center + new Vector3(Random.Range(-radius, radius), Random.Range(2f, 4f), Random.Range(-radius, radius));

        // instantiate a new object
        if (gameController.IsGamePlaying()) {
            GameObject.Instantiate(prefab, pos, prefab.transform.rotation);
            audios[1].PlayOneShot(audios[1].clip, whooshVolume);
            yield return new WaitForSeconds(audios[1].clip.length);
        }

        // start dissolving the old object
        Dissolve = true;
        yield return null;
    }

    void Update()
    {
        // update DissolvePercent when the object is dissolving
        if (Dissolve) {
            DissolvePercent += DissolveSpeed * Time.deltaTime;
            rend.material.SetFloat("DissolvePercent", DissolvePercent);
            if (DissolvePercent > 1) {
                Destroy(gameObject);
            }
        }
    }

}
