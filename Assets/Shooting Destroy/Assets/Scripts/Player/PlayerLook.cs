using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera camera;
    //khai báo biến nhân giá trị thay đổi trái phải từ chuột
    private float xRotation = 0f;

    //độ nhậy khi xay camera;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    private void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //góc quay  camera lên xuống
        xRotation -=(mouseY * Time.deltaTime ) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //Áp dụng giá trị biến đổi cho camera
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //Chỉnh hướng xoáy camera  nhìn trái và phải
        transform.Rotate(Vector3.up *(mouseX * Time.deltaTime) * xSensitivity);
    }
}
