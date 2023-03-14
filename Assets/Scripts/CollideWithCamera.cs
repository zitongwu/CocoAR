using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithCamera : MonoBehaviour
{
    private Camera cam;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "MainCamera")
        {
            audioSource.PlayOneShot(audioSource.clip, 1f);
        }
    }
}
