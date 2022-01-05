using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    public bool isBroken = false;
    public GameObject halo;
    public bool moving = false;
    public float health;
    public float initialHealth;


    private void Start()
    {
    }

    public void Update()
    {

    }

    public void Break()
    {
        isBroken = true;
        Debug.Log("Activating halo.");
        halo.SetActive(true);
    }

    public void Repair()
    {
        Debug.Log("Called Repair()");
        isBroken = false;
        health = initialHealth;
        halo.SetActive(false);
    }
}
