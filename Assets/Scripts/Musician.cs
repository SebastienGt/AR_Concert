using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Musician : MonoBehaviour
{
    public List<FlagCube> flags;
    FlagCube biggestFlag;
    public Transform statsUI;
    public Instrument instrument;
    public GameObject wearableInstrument;
    public AudioClip playingAudioClip;
    public AudioSource audioSource;
    public bool playing;
    public float healthDecayingSpeed;

    private float t;

    private readonly float START_MOVING_INSTRUMENT_DIST = 4.0f;
    private readonly float START_PLAYING_DIST = 2.0f;
    private readonly float LERP_SPEED = 1.0f;
    private readonly float FLAG_DIST_THRESH = 10.0f;

    void Start()
    {
        statsUI.gameObject.SetActive(false);
        playing = false;
        playingAudioClip = null;
        GetComponent<Animator>().enabled = false;
    }

    void Update()
    {
        if (statsUI.gameObject.activeSelf)
        {
            updateUIPosition();
        }
    }

    void BreakInstrumentStopPlaying()
    {
        instrument.health = 0;
        playing = false;
        audioSource.Pause();
        instrument.gameObject.SetActive(true);
        wearableInstrument.gameObject.SetActive(false);
        statsUI.gameObject.SetActive(false);
        instrument.Break();
        GetComponent<Animator>().enabled = false;
        instrument.transform.position = instrument.initialPos;
    }

    void FixedUpdate()
    {
        float highestScale = 0.0f;
        biggestFlag = null;
        foreach (FlagCube flagCube in flags)
        {
            if (flagCube.gameObject.activeSelf) {
                flagCube.halo.enabled = false;
                float scale = flagCube.transform.localScale.x;
                float dist = Vector3.Distance(transform.position, flagCube.transform.position);
                if (dist < FLAG_DIST_THRESH && scale > highestScale)
                {
                    highestScale = scale;
                    biggestFlag = flagCube;
                }
            }  
        }
        if (biggestFlag != null)
        {
            biggestFlag.halo.enabled = true;
            if (playing && biggestFlag.clip != playingAudioClip)
            {
                BreakInstrumentStopPlaying();
            }
            playingAudioClip = biggestFlag.clip;
        }
        if (playing) 
        {
            instrument.health -= healthDecayingSpeed * Time.fixedDeltaTime;
            statsUI.Find("InstrumentHealthBar").Find("FillBar").GetComponent<Image>().fillAmount = instrument.health / instrument.initialHealth;
            if (instrument.health <= 0) {
                BreakInstrumentStopPlaying();
            }
        } 
        else if (instrument.moving)
        {
            instrument.transform.position = Vector3.Lerp(instrument.transform.position, transform.position, t * LERP_SPEED);
            if (Vector3.Distance(instrument.transform.position, transform.position) <= START_PLAYING_DIST)
            {
                statsUI.gameObject.SetActive(true);
                instrument.moving = false;
                instrument.gameObject.SetActive(false);
                wearableInstrument.gameObject.SetActive(true);
                playing = true;
                if (!audioSource.isPlaying) {
                    Debug.Log("Starting to play.");
                    GetComponent<Animator>().enabled = true;
                    audioSource.clip = playingAudioClip;
                    audioSource.Play();
                }
            }
            t += Time.fixedDeltaTime;
        }
        else if (playingAudioClip != null && instrument.gameObject.activeSelf && !instrument.isBroken && Vector3.Distance(instrument.transform.position, transform.position) < START_MOVING_INSTRUMENT_DIST)
        {
            instrument.moving = true;
            instrument.initialPos = instrument.transform.position;
            t = 0;
        }
    }


    public void updateUIPosition() {
        // Offset position above object bbox (in world space)
        float offsetPosY = transform.position.y + 2*GetComponentInChildren<Collider>().bounds.extents.y;

        // Final position of marker above GO in world space
        Vector3 offsetPos = new Vector3(transform.position.x, offsetPosY, transform.position.z);
        
        // Calculate *screen* position (note, not a canvas/recttransform position)
        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);
        
        // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        RectTransformUtility.ScreenPointToLocalPointInRectangle(statsUI.parent.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);
        
        // Set
        statsUI.localPosition = canvasPos;
    }

}
