using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private Camera UICamera;
    private GameObject StartInterface;
    private bool Done;

    // Start is called before the first frame update
    void Awake()
    {
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        StartInterface = GameObject.FindGameObjectWithTag("StartInterface");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
        //Ray ray = UICamera.ScreenPointToRay(Input.GetTouch(0).position);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = UICamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if(hit.transform.gameObject == this.transform.gameObject) {
                    StartInterface.SetActive(false);
                }
            }
        }
    }

}