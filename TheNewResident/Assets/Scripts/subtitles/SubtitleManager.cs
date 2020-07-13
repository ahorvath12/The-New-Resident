using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public GameObject voice, player, flash;
    public GameObject[] subs;
    public GameObject[] caught;
    public bool beenCaught;
    
    private AudioSource voiceAudio;
    private int indexSub, indexCaught;
    private bool hasShown;

    // Start is called before the first frame update
    void Start()
    {
        indexSub = 0;
        indexCaught = 0;
        hasShown = false;
        voiceAudio = voice.GetComponent<AudioSource>();

        foreach(GameObject go in subs)
        {
            go.GetComponent<Text>().enabled = false;
        }

        foreach(GameObject go in caught)
        {
            go.GetComponent<Text>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (voiceAudio.isPlaying && !hasShown)
        {
            if (beenCaught)
            {
                flash.SetActive(false);
                caught[indexCaught].GetComponent<Text>().enabled = true;
                caught[indexCaught].GetComponent<Animator>().SetTrigger("FadeIn");
                indexCaught++;
            }
            else
            {
                subs[indexSub].GetComponent<Text>().enabled = true;
                subs[indexSub].GetComponent<Animator>().SetTrigger("FadeIn");
                indexSub++;
            }
            hasShown = true;
        }
        else if (!voiceAudio.isPlaying)
        {
            hasShown = false;
            if (beenCaught)
            {
                caught[indexCaught - 1].GetComponent<Animator>().SetTrigger("FadeOut");
                caught[indexCaught-1].GetComponent<Text>().enabled = false;
            }
            else
            {
                subs[indexSub-1].GetComponent<Animator>().SetTrigger("FadeOut");
                subs[indexSub-1].GetComponent<Text>().enabled = false;
            }
        }
    }
    
    
}
