using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFlash : MonoBehaviour
{
    public GameObject panel, cameraAudio, text, instructions, player;
    public GameObject[] movingMannequins;
    public bool enableFlash;

    private AudioSource cameraFlash;
    private Image img;
    private int uses;
    private bool showInstructions;
    private float mult = 0.01f;
    private Vector3 curPos, newPos;


    // Start is called before the first frame update
    void Start()
    {
        uses = 3;
        showInstructions = false;
        text.GetComponent<Text>().enabled = false;
        img = panel.GetComponent<Image>();
        cameraFlash = cameraAudio.GetComponent<AudioSource>();
        var temp = text.GetComponent<Text>().color;
        temp.a = -1f;
        instructions.GetComponent<Text>().color = temp;
        curPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = curPos;
        curPos = player.transform.position;
        var tempColor = img.color;
        if (enableFlash && curPos != newPos)
        {
            text.GetComponent<Text>().enabled = true;
            text.GetComponent<Text>().text = "Flash Bulbs: " + uses;
            showInstructions = !showInstructions;
        }

        if (showInstructions)
        {
            var tempColor2 = instructions.GetComponent<Text>().color;
            if (tempColor2.a >= 1.2f)
                mult *= -1;
            tempColor2.a += mult;
            instructions.GetComponent<Text>().color = tempColor2;
            
        }

        if (enableFlash && Input.GetKeyDown("space") && uses > 0)
        {
            cameraFlash.Play();
            tempColor.a = 1f;
            img.color = tempColor;

            foreach (GameObject go in movingMannequins)
            {
                go.GetComponent<DollController>().ReturnToOrigin();
            }

            uses -= 1;            
        }
        if (tempColor.a > 0f)
        {
            tempColor.a -= 0.009f;
            img.color = tempColor;
        }

    }
   
}
