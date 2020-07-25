using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Interact : MonoBehaviour
{
    public GameObject player, playerController, playerCamera, glassAudio, door;
    public GameObject instruction, list, panel, note;
    public GameObject[] mannequins, lights;

    private bool allow, pickedUp;
    private float max = 0.89f;
    private float min = 0f;
    private float mult = 0.003f;

    // Start is called before the first frame update
    void Start()
    {
        allow = false;
        pickedUp = false;
        list.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allow)
        {

            //instruction.GetComponent<Text>().enabled = true;

            //var tempColor = instruction.GetComponent<Text>().color;
            //if (tempColor.a >= max || tempColor.a <= min)
            //    mult *= -1f;
            //tempColor.a += mult;
            //instruction.GetComponent<Text>().color = tempColor;
        }
        else
        {
            instruction.GetComponent<Text>().enabled = false;
        }

        if (allow && Input.GetKeyDown("e"))
        {
            ShowList(true);
            allow = false;
            pickedUp = true;
        }
        else if (pickedUp && Input.GetKeyDown("e") && !GameObject.Find("Voice").GetComponent<AudioSource>().isPlaying)
        {
            ShowList(false);
            pickedUp = false;

            door.GetComponent<Escape>().AllowEscape();
            glassAudio.GetComponent<AudioSource>().Play();
            GameObject.Find("Flash").GetComponent<CameraFlash>().enableFlash = true;
            foreach (GameObject go in lights)
            {
                go.GetComponent<Light>().enabled = false;
            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == player && !pickedUp)
        {
            allow = true;
            instruction.GetComponent<Text>().enabled = true;
            //instruction.GetComponent<Animator>().SetTrigger("FadeIn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            allow = false;
            instruction.GetComponent<Text>().enabled = false;
            //instruction.GetComponent<Animator>().SetTrigger("FadeOut");
        }
    }

    private void ShowList(bool show)
    {
        list.GetComponent<Text>().enabled = show;
        note.GetComponent<Text>().enabled = show;
        panel.GetComponent<Image>().enabled = show;
        playerController.GetComponent<FirstPersonController>().enabled = !show;
        playerCamera.GetComponent<MouseLook>().enabled = !show;
        
        if (!show)
        {
            foreach (GameObject go in mannequins)
            {
                go.GetComponent<DollController>().CanLook();
                go.GetComponent<DollController>().CanMove();
                go.GetComponent<AudioSource>().enabled = true;
            }
        }
        else
        {
            GameObject.Find("Voice").GetComponent<VoiceManager>().NeedToLeave();
        }
       
    }
}
