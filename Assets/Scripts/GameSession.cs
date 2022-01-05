using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [HideInInspector]
    public static GameSession current;
    [HideInInspector]
    public Musician musician;
    [HideInInspector]
    public Instrument instrument;
    [HideInInspector]
    public bool musicianBusy = false;
    public float actionTriggerDistance = .5f;
    public LayerMask touchablesLayer;

    [HideInInspector]
    public bool musicianTracked = false;
    [HideInInspector]
    public bool shopTracked = false;
    [HideInInspector]
    public bool instrumentTracked = false;


    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetShopTrackedState(bool tracked)
    {
        shopTracked = tracked;
    }

    public void SetMusicianTrackedState(bool tracked)
    {
        musicianTracked = tracked;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
