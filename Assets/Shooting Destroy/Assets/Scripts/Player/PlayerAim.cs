using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class PlayerAim : MonoBehaviour
{
    public GameObject weapon;
    private bool turnAim;
    public Camera cam;
    // FOV values
    public float normalFOV = 40f;
    public float zoomedFOV = 20f;
    void Start()
    {
        cam = Camera.main;
        // Set the initial FOV
        SetFOV(normalFOV);
    }
    public void Aim()
    {
        turnAim = !turnAim;
        weapon.GetComponent<Animator>().SetBool("IsAim", turnAim); 
        ToggleZoom();
    }
   
    void ToggleZoom()
    {
        // Toggle between normal and zoomed FOV
        if (cam.fieldOfView == normalFOV)
        {
            SetFOV(zoomedFOV);
        }
        else
        {
            SetFOV(normalFOV);
        }
    }
    void SetFOV(float newFOV)
    {
        cam.fieldOfView = newFOV;
    }

}
