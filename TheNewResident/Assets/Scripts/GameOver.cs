using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject panel;
    public Text text;

    private float max = 1f;
    private float min = 0.2f;
    private float mult = 0.002f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //var tempColor = text.color;
        //if (tempColor.a >= max || tempColor.a <= min)
        //    mult *= -1f;
        //tempColor.a += mult;
        //text.color = tempColor;

        if (Input.anyKey)
        {
            panel.GetComponent<Animator>().SetTrigger("FadeOut");
            SceneManager.LoadScene("Menu");
        }
    }
}
