using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public GameObject player;
    public AudioClip caughtGrunt, breathing, leave;
    public AudioClip[] voiceLines;

    private AudioSource audioVoice;
    bool hasPlayed, caught, breath, left;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
        caught = false;
        breath = false;
        left = false;
        audioVoice = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 10f && !hasPlayed)
        {
            Debug.Log("Playing next clip");
            audioVoice.clip = voiceLines[0];
            audioVoice.Play();
            hasPlayed = !hasPlayed;
        }
    }

    public void NeedToLeave()
    {
        if (!left)
        {
            audioVoice.clip = leave;
            audioVoice.Play();
            left = true;
        }
    }
    
    public void Caught()
    {
        if (!caught)
        {
            audioVoice.clip = caughtGrunt;
            audioVoice.Play();
            caught = true;
        }
    }

    public void Breathing()
    {
        if (!breath)
        {
            audioVoice.volume /= 3f;
            audioVoice.clip = breathing;
            audioVoice.Play();
            breath = true;
        }
    }
}
