using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPerson : MonoBehaviour
{
    public float maxDistance;
    public float minDistance;

    public Instrument instrument;
    private Vector3 initialPos;
    private bool movingToInstrument;
    private bool movingToInit;
    private float t;
    
    public float LERP_SPEED = 0.5f;

    void Start()
    {
        GetComponent<Animator>().enabled = false;
    }

    void FixedUpdate()
    {
        if (instrument)
        {
            var dist = Vector3.Distance(transform.position, instrument.transform.position);
            if (!movingToInstrument && dist < maxDistance && instrument.isBroken)
            {
                initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                t = 0;
                movingToInstrument = true;
                GetComponent<Animator>().enabled = true;
                transform.LookAt(instrument.transform.position);
            } 
            else if (movingToInstrument)
            {
                transform.position = Vector3.Lerp(transform.position, instrument.transform.position, LERP_SPEED * t);
                t += Time.fixedDeltaTime;
                if (dist < minDistance) {
                    instrument.Repair();
                    movingToInstrument = false;
                    movingToInit = true;
                    t = 0;
                }
                transform.LookAt(instrument.transform.position);
            } 
            else if (movingToInit)
            {
                transform.position = Vector3.Lerp(transform.position, initialPos, LERP_SPEED * t);
                t += Time.fixedDeltaTime;
                transform.LookAt(initialPos);
                if (Vector3.Distance(transform.position, initialPos) < minDistance)
                {
                    transform.position = initialPos;
                    movingToInit = false;
                    GetComponent<Animator>().enabled = false;
                    t = 0;
                }
            }
        }
    }
}
