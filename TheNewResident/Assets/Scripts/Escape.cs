using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    public GameObject player, instructions, hideScreen, audio;

    private bool collided;
    private float max = 0.89f;
    private float min = 0f;
    private float mult = 0.003f;

    // Start is called before the first frame update
    void Start()
    {
        collided = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        instructions.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collided)
        {
            var tempColor = instructions.GetComponent<Text>().color;
            if (tempColor.a >= max || tempColor.a <= min)
                mult *= -1f;
            tempColor.a += mult;
            instructions.GetComponent<Text>().color = tempColor;
            audio.GetComponent<AudioSource>().Play();

            if (Input.GetKeyDown("e"))
            {
                hideScreen.GetComponent<Animator>().SetTrigger("FadeOut");
                SceneManager.LoadScene("Outro");
            }
        }
    }

    public void AllowEscape()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            collided = true;
            Debug.Log("collided");
            instructions.GetComponent<Text>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            collided = false;
            instructions.GetComponent<Text>().enabled = false;
        }
    }
}
