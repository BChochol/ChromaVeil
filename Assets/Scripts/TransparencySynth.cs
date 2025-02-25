using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencySynth : MonoBehaviour
{
    public Material material;
    public Camera camera;
    public LayerMask wallMask;

    void Update()
    {
        Vector3 dir = camera.transform.forward;
        var ray = new Ray(transform.position, -dir);
        
        if (Physics.Raycast(ray,  Mathf.Infinity, wallMask))
        {
            material.SetFloat("_Visibility", 1f);
        }
        else
        {
            material.SetFloat("_Visibility", 0f);
        }
        
        Vector3 view = camera.WorldToViewportPoint(transform.position);
        material.SetVector("_playerPosition", view);
    }
}
