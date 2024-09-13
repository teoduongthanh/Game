﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorID : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("character_nearby", doorOpen);
        Destroy(gameObject);
    }
    
}
