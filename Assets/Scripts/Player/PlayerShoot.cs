using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    private RaycastWeapon weapon;

    void Start()
    {
        weapon = GetComponentInChildren<RaycastWeapon>();
    }
    //Phương thức sẽ bắn ra một tia đỏ để thử nghiệm đường đạn
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.StartFiring();

        }
        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }
        if (Input.GetMouseButtonUp(0))
        {
            weapon.StopFiring();
        }
    }
   
}


