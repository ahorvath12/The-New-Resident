using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerFall : MonoBehaviour
{
    public bool end;
    public GameObject panel, playerCamera, endCamera, voice, quitText;
    public GameObject[] mannequins;

    private Transform playerTransform, endTransform;
    private Animator anim;

    private int count;
    private bool endGame, changeScene, fadeOutAgain;

    private void Start()
    {
        count = 0;
        endGame = false;
        changeScene = false;
        fadeOutAgain = false;
        playerCamera.SetActive(true);
        endTransform = endCamera.transform;
        endCamera.SetActive(false);
        anim = panel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // knock player over
        if (end)
        {
            foreach(GameObject go in mannequins)
            {
                go.GetComponent<AudioSource>().enabled = false;
            }

            GameObject.Find("Dialogue").GetComponent<SubtitleManager>().beenCaught = true;
            playerCamera.GetComponent<MouseLook>().enabled = false;
            gameObject.GetComponent<FirstPersonController>().enabled = false;

            playerTransform = gameObject.transform;
            Vector3 newRotate = playerTransform.rotation.eulerAngles;
            if (newRotate.x > -90.0f && newRotate.z < 90f)
            {
                newRotate.x -= 2f;
                newRotate.z += 0.1f;
            }
            else
            {
                end = !end;
                endGame = true;
            }

            Vector3 newPos = playerTransform.position;
            if (newPos.y > 0.5f)
                newPos.y -= 0.2f;

            playerTransform.rotation = Quaternion.Euler(newRotate);
            playerTransform.position = newPos;
            quitText.GetComponent<Animator>().SetTrigger("FadeIn");
        }

        // move the player to mannequin position
        if (endGame)
        {
            anim.SetTrigger("FadeOut");

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("End 0"))
            { 
                playerCamera.transform.position = endTransform.position;
                playerCamera.transform.rotation = endTransform.rotation;
                anim.SetTrigger("FadeIn");
                //mult *= -1f;
                changeScene = true;

            }
        }

        // wait and load scene
        if (changeScene)
        {
            end = false;
            endGame = false;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("FadeIn2"))
                voice.GetComponent<VoiceManager>().Breathing();
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
                fadeOutAgain = true;
            else if (fadeOutAgain && anim.GetCurrentAnimatorStateInfo(0).IsName("FadeOut3"))
            {
                anim.SetTrigger("End");
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("End") && !voice.GetComponent<AudioSource>().isPlaying)
                SceneManager.LoadScene("GameOver");

        }
            
        if (Input.GetKeyDown("return") && (end || endGame || changeScene))
            SceneManager.LoadScene("Menu");
        
    }
    
    
}
