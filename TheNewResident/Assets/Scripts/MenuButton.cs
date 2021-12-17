using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    private Button button;
    private Text text;

    // Start is called before the first frame update
    void Awake()
    {
        button = this.GetComponent<Button>();
        text = (Text)button.GetComponentInChildren<Text>();
    }


    public void Enter()
    {
        text.color = Color.red;
    }

    public void Exit()
    {
        text.color = Color.white;
    }
}
