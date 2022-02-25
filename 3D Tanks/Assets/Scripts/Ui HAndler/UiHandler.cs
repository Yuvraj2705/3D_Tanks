using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [SerializeField] GameObject Minimap;
    [SerializeField] GameObject WalkMan;
    [SerializeField] GameObject Player;

    [SerializeField] AudioClip[] Songs;

    bool minimapCheck;
    bool musicCheck;
    bool isMusicPlaying;

    int musicCounter = 0;

    Animator ani;
    AudioSource walkmanSpeaker;

    void Start()
    {
        ani = Player.GetComponent<Animator>();

        minimapCheck = true;
        musicCheck = true;
        isMusicPlaying = false;
        Minimap.SetActive(false);
        WalkMan.SetActive(false);
    }

    void Update()
    {
        MiniMapHandler();
        MusicHandler();
    }

    void MusicHandler()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(musicCheck)
            {
                ani.SetBool("MusicOn",true);
                WalkMan.SetActive(true);
                musicCheck = false;
                isMusicPlaying = true;
            }
            else
            {
                ani.SetBool("MusicOn",false);
                WalkMan.SetActive(false);
                musicCheck = true;
                isMusicPlaying = false;
            }
        }
    }

    void MusicChanger()
    {
        if(isMusicPlaying)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                walkmanSpeaker.PlayOneShot(Songs[musicCounter]);
                if(musicCounter == Songs.Length)
                {
                    musicCounter = 0;
                }
                {
                    musicCounter += 1;
                }
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }

    void MiniMapHandler()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(minimapCheck)
            {
                Minimap.SetActive(true);
                minimapCheck = false;
            }
            else
            {
                Minimap.SetActive(false);
                minimapCheck = true;
            }
        }
    }
}
