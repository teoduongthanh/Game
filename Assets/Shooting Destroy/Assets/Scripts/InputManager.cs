using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //khai báo các biến cần nhận vào từ bàn thông qua đối tượng scripts khác 
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    //Lấy thành phần động năng di chuyển
    private PlayerMotor motor;
    //Khai báo giá trị đầu vào từ chuột tới PlayerLook.cs
    private PlayerLook look;

    private PlayerAim aim;
    void Awake()
    {
        //tạo biến bằng với di chuyển được nhân từ bàn phím bằng mới đầu vào người chơi;
        playerInput = new PlayerInput(); 
        //sử dụng Action từ input system để xác định các hành động di chuyển
        onFoot =  playerInput.OnFoot;
        //sau đó thay đổi cập nhập cố định từ motor
        motor = GetComponent<PlayerMotor>();
        //Nếu nhận giá trị từ bàn phím là jump thì sẽ thục thi Phương thức Jump() Được tạo từ PlayerMotor.cs
        onFoot.Jump.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        aim = GetComponent<PlayerAim>();
        onFoot.Aim.performed += ctx => aim.Aim();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //yêu cầu player di chuyển bằng cachs sử dụng giá trị từ hành động di chuyển.
        motor.ProcesMove(onFoot.Movement.ReadValue<Vector2>());


    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        //gọi kích hoạt khi hành động từ được nhập từ bàn phím
        onFoot.Enable();
    }
    private void OnDisable()
    {
        //gọi kích hoạt khi hành động dừng
        onFoot.Disable();
    }
}
