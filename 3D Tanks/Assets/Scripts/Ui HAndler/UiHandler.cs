using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour
{
    [SerializeField] GameObject Minimap;
    [SerializeField] GameObject WalkMan;
    [SerializeField] GameObject Player;

    [SerializeField] GameObject minimapCam;

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
        musicCounter = 0;
        minimapCheck = true;
        musicCheck = true;
        isMusicPlaying = false;
        Minimap.SetActive(false);
        WalkMan.SetActive(false);
        minimapCam.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

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

        //MusicChanger();
    }

    void MusicChanger()
    {
        if(isMusicPlaying)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                walkmanSpeaker.PlayOneShot(Songs[musicCounter]);
                musicCounter += 1;
                if(musicCounter == Songs.Length)
                {
                    musicCounter = 0;
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
                minimapCam.SetActive(true);
                minimapCheck = false;
            }
            else
            {
                Minimap.SetActive(false);
                minimapCam.SetActive(false);
                minimapCheck = true;
            }
        }
    }
}
