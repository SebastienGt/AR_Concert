using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public float happinessIncreaseSpeed = 2f;
    public float max_distance = 0.2f;
    private bool interacting = false;

    private Instrument instrument;

    // Update is called once per frame
    void Update()
    {
        if (!instrument)
        {
            instrument = GameSession.current.instrument;
            return;
        }

        transform.position = transform.parent.position;

        if (!interacting)
        {
            if (Vector3.Distance(transform.position, instrument.transform.position) <= max_distance && GameSession.current.instrumentTracked && GameSession.current.shopTracked)
            {
                StopAllCoroutines();
                StartCoroutine(PerformRepair());
                interacting = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, instrument.transform.position) > max_distance || !GameSession.current.instrumentTracked || !GameSession.current.shopTracked)
            {
                StopAllCoroutines();
                StartCoroutine(ReturnToPlace());
                interacting = false;
            }
        }

    }

    IEnumerator PerformRepair()
    {
        float lerpParam = 0;
        Transform shopTransform = transform.GetChild(0);
        Vector3 initialPos = shopTransform.position;

        Transform instrumentTransform = GameSession.current.instrument.transform;
        shopTransform.LookAt(instrumentTransform);
        float instrumentWidth = instrumentTransform.GetComponentInChildren<Collider>().bounds.extents.x *  2;

        Vector3 initialShopFwd = shopTransform.forward;
        Vector3 dstShopFwd = (instrumentTransform.position - shopTransform.position).normalized;
        Vector3 initialInstrumentFwd = instrumentTransform.forward;
        Vector3 dstInstrumentFwd = (shopTransform.position - instrumentTransform.position).normalized;

        while (true)
        {
            shopTransform.position = Vector3.Lerp(initialPos, instrumentTransform.position - shopTransform.forward * instrumentWidth, lerpParam);
            instrumentTransform.forward = Vector3.Lerp(initialInstrumentFwd, dstInstrumentFwd, 2f * lerpParam);
            //shopTransform.forward = Vector3.Lerp(initialShopFwd, dstShopFwd, lerpParam);
            
            lerpParam += Time.fixedDeltaTime;

            GameSession.current.instrument.isBroken = false;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ReturnToPlace()
    {
        float lerpParam = 0;
        Transform shopTransform = transform.GetChild(0);
        Transform instrumentTransform = GameSession.current.instrument.transform;
        Vector3 initialPos = shopTransform.position;
        shopTransform.LookAt(transform);
        Vector3 initialInstrumentFwd = instrumentTransform.forward;
        //Vector3 initialShopFwd = shopTransform.forward;

        shopTransform.GetComponent<Animator>().SetTrigger("happiness");
        instrumentTransform.GetComponent<Animator>().SetTrigger("happiness");

        while (lerpParam <= 1f)
        {
            shopTransform.position = Vector3.Lerp(initialPos, transform.position, lerpParam);
            instrumentTransform.forward = Vector3.Lerp(initialInstrumentFwd, -instrumentTransform.parent.forward, 2f * lerpParam);
            //shopTransform.forward = Vector3.Lerp(initialShopFwd, -shopTransform.parent.forward, lerpParam);
            lerpParam += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }


        lerpParam = 0;
        Vector3 initialShopFwd = shopTransform.forward;
        while (lerpParam <= 1f)
        {
            shopTransform.forward = Vector3.Lerp(initialShopFwd, -transform.forward, lerpParam);
            lerpParam += 2f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
