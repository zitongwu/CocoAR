/// Author: Zitong Wu
/// Adapted from Unity Documentation UI.GraphicRaycaster.Raycast

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class UIStartButton : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    
    private bool buttonPressed = false;
    private AudioSource audioSource;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = transform.parent.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = transform.parent.GetComponent<EventSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Check if the left Mouse button is clicked
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //Play Button Sound and Disable Button on click
            if (results.Count > 0) {
                if (results[0].gameObject.name == "ButtonText") {
                    StartCoroutine(PlayAudioAndDisableButton());
                }
            }
        }
    }

    IEnumerator PlayAudioAndDisableButton() {
        audioSource.Play();
        yield return new WaitForSeconds(0.5f);
        buttonPressed = true;
        gameObject.SetActive(false);
    }

    public bool IsButtonPressed() 
    {
        return buttonPressed;
    }
}