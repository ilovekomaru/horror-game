using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Transform Player;
    public float pickupRange = 2.0f;
    public float markupRange = 10.0f;
    public GameObject pickupMessage;
    public GameObject PlayerScript;


    new Light light;
    void Start()
    {
        //particleSystem = GetComponentInChildren<ParticleSystem>();
        light = GetComponentInChildren<Light>();
        pickupMessage.SetActive(false);
        light.intensity = 0;
        
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0f, 0), 10);
    }

    void Update()
    {
        if (Time.time > PlayerScript.GetComponent<Player>().currentCollectTime + PlayerScript.GetComponent<Player>().collectTime)
        {
            light.intensity = Mathf.Clamp(1 + (Time.time - (float)PlayerScript.GetComponent<Player>().collectTime) / 10, 1, 3);
        }
        else if (Vector3.Distance(transform.position, Player.position) < markupRange)
        {
            light.intensity = 1 - Mathf.Clamp01(Vector3.Distance(transform.position, Player.position) / markupRange);
            
            //particleSystem.emission.rateOverTime.Evaluate(particleMultiplyer * (18f - Vector3.Distance(transform.position, Player.position)));
        }
        if (Vector3.Distance(transform.position, Player.position) < pickupRange)
        {
            pickupMessage.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                PlayerScript.GetComponent<Player>().collectedNotesCount += 1;
                PlayerScript.GetComponent<Player>().currentCollectTime = Time.time;
                transform.position = new Vector3(0, -10, 0);
                light.intensity = 0;
            }
        }
        else
        {
            pickupMessage.SetActive(false);
        }
    }
}
