using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicianPlayScript : MonoBehaviour
{
    public readonly float DIST_THRESHOLD = 5.0f;
    public GameObject Instrument;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnPointerClick()
    {
        var dist = Vector3.Distance(transform.position, Instrument.transform.position);
        if (dist < DIST_THRESHOLD)
        {
            Debug.Log("Instrument close enough. Will play song.");
        }
        else
        {
            Debug.Log("Instrument too far. Cannot play.");
        }
        Debug.Log("CLICKED ON MUSICIAN.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
