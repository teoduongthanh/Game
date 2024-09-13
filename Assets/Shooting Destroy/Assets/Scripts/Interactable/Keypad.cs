using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//kế thừa class cha

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //phương thức này sẽ ghi đề lên phương thức in class cha
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("character_nearby",doorOpen);
    }
}
