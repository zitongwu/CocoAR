/// Author: Zitong Wu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class GameController : MonoBehaviour
{
    public int targetScore = 1;

    private ARPlaneManager planeManager;
    private GameObject basketObj;
    private GameObject endInterfaceGround;
    private TextMeshProUGUI scoreBase;
    private UIStartButton startButton;
    private GameObject startText2;
    private BasketInner basketInnerScript;
    private GameObject startInterface;
    private GameObject instructionInterface;
    private GameObject endInterface;
    private GameObject startText;
    private GameObject endText;
    private AudioSource audioSource;
    private bool gameStarted = false;
    private Animator anim;
    private bool serveFood = false;
    private int step = 0;

    void Start() {
        startInterface = GameObject.Find("StartInterface"); 
        instructionInterface = GameObject.FindGameObjectWithTag("InstructionInterface");
        endInterface = GameObject.FindGameObjectWithTag("EndInterface"); 
        startText = GameObject.FindGameObjectWithTag("StartText");
        endText = GameObject.FindGameObjectWithTag("EndText");
        basketObj = GameObject.Find("AR Session Origin/AR Camera/Basket");
        startText2 = GameObject.Find("/UICanvas/StartText2");
        basketInnerScript = basketObj.transform.Find("BasketInner").gameObject.GetComponent<BasketInner>();
        startButton = GameObject.FindGameObjectWithTag("Button").gameObject.GetComponent<UIStartButton>();
        scoreBase = GameObject.FindGameObjectWithTag("ScoreNumberBase").gameObject.GetComponent<TextMeshProUGUI>();;
        endInterfaceGround = GameObject.FindGameObjectWithTag("EndInterfaceGround");
        planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        endInterfaceGround.SetActive(false); 
        planeManager.enabled = false;
        scoreBase.text = "/ "+ targetScore.ToString();
    }

    void Update()
    {
        // when the player has collected targetScore of cocos, end the game
        if (basketInnerScript.GetTotalScore() >= targetScore) {
            gameStarted = false;
        }

        // interface control

        // game start sequence: startText, instructionInterface, startText2
        if (startButton.IsButtonPressed() && step == 0) {
            StartCoroutine(ExecuteStep(startText, 2, true));
        }
        else if (!startText.activeSelf && step == 1) {
            StartCoroutine(ExecuteStep(instructionInterface, 6, true));
        }
        else if (!instructionInterface.activeSelf && step == 2) {
            // disable startInterface
            startInterface.SetActive(false);
            // display startText
            StartCoroutine(ExecuteStep(startText2, 2, true));
            // start game
            gameStarted = true;
            // enable planeManager to detect plane for intializing scene and cocos
            planeManager.enabled = true;
        }

        // game end sequence: endInterface, endText
        else if (!gameStarted && step == 3) {
            endInterfaceGround.SetActive(true);
            StartCoroutine(ExecuteStep(endInterface, 8, true));
        }
        else if (!endInterface.activeSelf && step == 4) {
            // destroy all cocos in the basket 
            List<GameObject> objs = basketInnerScript.GetCurrentColliders();
            StartCoroutine(DestroyListOfObjects(objs));
            // destroy basket
            basketObj.SetActive(false);
            // destry endInterface
            endInterfaceGround.SetActive(false);
            // display endText
            StartCoroutine(ExecuteStep(endText, 2, true));
            // enable serving food
            serveFood = true;
            // enable plane manager to detect table plane for serving food
            planeManager.enabled = true;
        }

    }

    /// for destroying all objects when the game ends
    IEnumerator DestroyListOfObjects(List<GameObject> objs) 
    {
        if (objs.Count > 0) {
            for (int i = 0; i < objs.Count;  i ++) {
                Destroy(objs[i]);
            }
        }
        yield return null;
    }

    /// display and destroy an object
    IEnumerator ExecuteStep(GameObject obj, float seconds, bool deactivate){
        step ++;
        obj.SetActive(true);
        yield return new WaitForSeconds(seconds);
        if (deactivate) {
            obj.SetActive(false);
        }
        yield return null;
    }

    public bool IsGamePlaying() {
        return gameStarted;
    }

    public bool IsServingFood() {
        return serveFood;
    }

    public int GetGameStep() {
        return step;
    }
}
