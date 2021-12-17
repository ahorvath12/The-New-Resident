using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject panel;
    public GameObject menu, credits;
    public GameObject[] buttons;
    public GameObject[] creditsPages;

    private Image img;
    private int index = 0;
    private float max = 0.937f;
    private float min = 0.841f;
    private float mult = 0.0001f; //windows
    //private float mult = 0.001f; //mac

    // Start is called before the first frame update
    void Start()
    {
        img = panel.GetComponent<Image>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        buttons[0].GetComponent<MenuButton>().Enter();
    }

    // Update is called once per frame
    void Update()
    {
        //var tempColor = img.color;
        //if (tempColor.a >= max || tempColor.a <= min)
        //    mult *= -1f;
        //tempColor.a += mult;
        //img.color = tempColor;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown("d"))
        {
            buttons[index].GetComponent<MenuButton>().Exit();
            index += 1;
            if (index >= buttons.Length)
                index = 0;
            buttons[index].GetComponent<MenuButton>().Enter();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown("a"))
        {
            buttons[index].GetComponent<MenuButton>().Exit();
            index -= 1;
            if (index < 0)
                index = buttons.Length - 1;
            buttons[index].GetComponent<MenuButton>().Enter();
        }

        if (Input.GetKeyDown("return"))
        {
            switch (index)
            {
                case 0:
                    PlayGame();
                    break;
                case 1:
                    QuitGame();
                    break;
                case 2:
                    StartCredits();
                    break;
            }
        }

        if (Input.GetKeyDown("escape") && credits.activeSelf)
            EndCredits();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void StartCredits()
    {
        credits.SetActive(true);
        menu.SetActive(false);
        StartCoroutine(RollCredits());
    }

    public void EndCredits()
    {
        StopCoroutine(RollCredits());
        credits.SetActive(false);
        menu.SetActive(true);
    }


    private IEnumerator RollCredits()
    {
        int index = 0;
        while (index < creditsPages.Length)
        {
            creditsPages[index].SetActive(true);
            yield return new WaitForSeconds(4);
            index++;
            creditsPages[index - 1].SetActive(false);
        }
        EndCredits();
    }
}
