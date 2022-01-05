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
    
    private readonly float LERP_SPEED = 1.0f;

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
            } 
            else if (movingToInit)
            {
                transform.position = Vector3.Lerp(transform.position, initialPos, LERP_SPEED * t);
                t += Time.fixedDeltaTime;
                if (Vector3.Distance(transform.position, initialPos) < minDistance)
                {
                    transform.position = initialPos;
                    movingToInit = false;
                    t = 0;
                }
            }
        }
    }
}
