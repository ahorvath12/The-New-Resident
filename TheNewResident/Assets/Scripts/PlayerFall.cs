using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerFall : MonoBehaviour
{
    public bool end;
    public GameObject panel, playerCamera, endCamera, voice;
    public GameObject[] mannequins;

    private Transform playerTransform, endTransform;
    private float rotateZ = 90.0f;
    private float positionY = 0.5f;
    private float mult = 0.001f; //windows
    //private float mult = 0.003f; //mac

    private int count;
    private bool endGame, changeScene;

    private void Start()
    {
        count = 0;
        endGame = false;
        changeScene = false;
        playerCamera.SetActive(true);
        endTransform = endCamera.transform;
        endCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool change = false;

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
        }

        // move the player to mannequin position
        if (endGame)
        {
            var tempColor = panel.GetComponent<Image>().color;
            if (tempColor.a < 1f || tempColor.a > 0f)
                tempColor.a += mult;
            panel.GetComponent<Image>().color = tempColor;

            if (tempColor.a >= 1f)
            { 
                playerCamera.transform.position = endTransform.position;
                playerCamera.transform.rotation = endTransform.rotation;
                mult *= -1f;
                changeScene = true;

            }
        }

        // wait and load scene
        if (changeScene)
        {
            end = false;
            endGame = false;
            var tempColor = panel.GetComponent<Image>().color;


            if (tempColor.a > 0.9f)
                voice.GetComponent<VoiceManager>().Breathing();

            if (tempColor.a < 1f || tempColor.a > 0f)
                tempColor.a += mult;
            //else if (tempColor.a >= 1f)
            //    Debug.Log("change scene");
            //SceneManager.LoadScene("GameOver");

            if (tempColor.a <= 0f)
            {
                mult *= -1f;
                count = 1;
            }
            if (!voice.GetComponent<AudioSource>().isPlaying)
            {
                SceneManager.LoadScene("GameOver");
            }


            panel.GetComponent<Image>().color = tempColor;
            

        }

        //if (change)
        //{
        //    changeScene = !changeScene;
        //    StartCoroutine("EndGame");
        //}

    }

    private IEnumerator EndGame()
    {
        Debug.Log("in coroutine");
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("GameOver");
    }
    
}
