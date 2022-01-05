 using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Musician : MonoBehaviour
{
    public Transform statsUI;
    public Instrument instrument;
    public AudioSource audioSource;
    public bool playing;
    public float healthDecayingSpeed = 1f;
    private float instrumentHealth;
    public AudioClip playingAudioClip;

    public float initialInstrumentHealth = 100.0f;
    private readonly float START_MOVING_INSTRUMENT_DIST = 4.0f;
    private readonly float START_PLAYING_DIST = 2.0f;
    private readonly float LERP_SPEED = 1.0f;

    void Start()
    {
        enableUI();
        playing = false;
        instrumentHealth = initialInstrumentHealth;
    }

    void Update()
    {
        if (statsUI.gameObject.activeSelf && GetComponent<Renderer>().isVisible)
        {
            updateUIPosition();
        }
    }

    void FixedUpdate()
    {

        float dist = instrument ? Vector3.Distance(instrument.transform.position, transform.position) : 1000.0f;
        if (playing) 
        {
            instrumentHealth -= healthDecayingSpeed * Time.deltaTime;
            statsUI.Find("InstrumentHealthBar").Find("FillBar").GetComponent<Image>().fillAmount = instrumentHealth / initialInstrumentHealth;
        } 
        else if (dist < START_MOVING_INSTRUMENT_DIST) 
        {
            Debug.Log("Instrument close enough, beginning Lerp.");
            Debug.Log("d(mus, ins) = " + dist);
            instrument.transform.position = Vector3.Lerp(instrument.transform.position, transform.position, LERP_SPEED);
            if (instrument.gameObject.activeSelf && Vector3.Distance(instrument.transform.position, transform.position) <= START_PLAYING_DIST)
            {
                instrument.gameObject.SetActive(false);
                playing = true;
                if (!audioSource.isPlaying) {
                    Debug.Log("Starting to play.");
                    if (playingAudioClip == null)
                    {
                        Debug.Log("Null audio clip");
                    }
                    audioSource.clip = playingAudioClip;
                    audioSource.Play();
                }
            }
        }
        else
        {
            Debug.Log("d(mus, ins) = " + dist);
        }
        
    }

    public void enableUI() {
        statsUI.gameObject.SetActive(true);
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
