using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    new Light light;
    float runEnergy = 100, lightEnergy = 100;
    double timeFlag, lightCooldown;
    public TextMeshProUGUI lightEnergyText;
    public Transform target;
    public int collectedNotesCount = 0;
    public int totalNotesCount = 5;
    public double currentCollectTime = 0;
    public double collectTime = 30f;


    void Start()
    {
        light = GetComponentInChildren<Light>();
        light.enabled = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,.7f,0), 12);
    }


    void Update()
    {
        Debug.Log(collectedNotesCount);
        light.intensity = (float)0.2 + Mathf.Sqrt(lightEnergy) / 10;
        if (lightCooldown <= 0)
        {
            lightEnergyText.text = lightEnergy.ToString();
            if (Input.GetKeyDown(KeyCode.E))
            {
                light.enabled = !light.enabled;
            }
        }
        else
        {
            lightEnergyText.text = "..." + lightCooldown.ToString("F0");
        }
        
        if (Time.time > timeFlag) 
        {
            lightEnergy += light.enabled ? -1 : 1;
            timeFlag = Time.time + 0.2;
        }
        if (lightEnergy > 100)
        {
            lightEnergy = 100; 
        }
        if (lightEnergy < 0)
        {
            light.enabled = false;
            lightCooldown = 3;
        }
        lightCooldown -= Time.deltaTime;
    }
}
