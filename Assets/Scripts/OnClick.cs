using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    // ReSharper disable InconsistentNaming

    private Transform target;
    private ParticleSystem particle;

    private void Start()
    {
        target = GetComponent<Transform>();
        particle = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            mousePos.z = -3;
            target.position = mousePos;

            particle.Play();
            
            // Debug.Log("Clicked");
        }
    }
}
