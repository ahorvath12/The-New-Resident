using UnityEngine;
using UnityEngine.AI;

public class DollController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject player, nearCollider, fp, voice;
    public GameObject mesh;
    public AudioClip[] clips;

    private Animator anim;
    private Renderer meshRend;
    private AudioSource audioSource;
    private int poseNum;
    private bool move, start;
    private Vector3 startingPos, curPos, lastPos;
    private Quaternion startingRot;
    private int flash;

    private static bool caught = false;

    void Start()
    {
        flash = 0;
        poseNum = 0;
        caught = false;
        move = false;
        start = false;
        startingPos = gameObject.transform.position;
        startingRot = gameObject.transform.rotation;
        anim = GetComponent<Animator>();
        meshRend = mesh.GetComponent<Renderer>();
        meshRend.enabled = true;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        lastPos = curPos;
        curPos = gameObject.transform.position;
        if (curPos != lastPos && !audioSource.isPlaying)
        {
            int index = Random.Range(0, clips.Length);
            audioSource.clip = clips[index];
            audioSource.Play();
        }

        if (!meshRend.isVisible && move)//if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;


            // move agent
            agent.SetDestination(player.transform.position);
            // change pose
            poseNum = Random.Range(1, 4);
            anim.SetInteger("pose", poseNum);

        }
        else if (meshRend.isVisible)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            if (start && !move)
            {
                move = true;
                anim.SetBool("canMove", true);
                GameObject.Find("Flash").GetComponent<CameraFlash>().enableFlash = true;
            }
            audioSource.Stop();
        }

        if (caught)
        {
            ReturnToOrigin();
            meshRend = null;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == nearCollider && !meshRend.isVisible)
        {
            move = false;
            anim.SetBool("isClose", true);
        }
        else if (collision.gameObject == player && !meshRend.isVisible)
        {
            caught = true;
            voice.GetComponent<VoiceManager>().Caught();
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            fp.GetComponent<PlayerFall>().end = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == nearCollider && !meshRend.isVisible)
        {
            move = true;
            anim.SetBool("isClose", false);
        }
    }


    public void CanLook()
    {
        anim.SetBool("look", true);
    }

    public void CanMove()
    {
        start = true;
    }

    public void ReturnToOrigin()
    {
        if (meshRend.isVisible)
        {
            gameObject.transform.position = startingPos;
            gameObject.transform.rotation = startingRot;
            anim.SetTrigger("return");
            anim.SetBool("canMove", false);
            anim.SetInteger("pose", 0);
            agent.speed += 1;
            move = false;
        }
    }
}
