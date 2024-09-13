using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVolocify;
    //biến dugnf để check player có đăng chạm đất không
    private bool IsGrounded;
    private float gravity = -9.8f;
    public float speed = 5f;

    public float jumpHeight =3f;
    //tạo biến xác định thời gina cúi ngồi xuống của player
    public float crouchTime =5f;
    private bool lerpCrouch, crouching, sprinting;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        //đảm bảo player đang ở trên mặt đất 
        IsGrounded = controller.isGrounded;

        if (lerpCrouch) 
        {
            crouchTime += Time.deltaTime;
            float p = crouchTime / 1;
            p *= p;
            if(crouching)
                controller.height  = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height,2, p);
            if(p > 1)
            {
                lerpCrouch = false;
                crouchTime = 0f;
            }
        }

    }
    public void Crouch()
    {
        
        crouching = !crouching;
        //sẽ cài đặt lại giá trị thời gian ngồi xuống lại bằng 0 sau mỗi lần ấn.
        crouchTime = 0;
        lerpCrouch = true;
    }
    public void Sprint()
    {
        //kiểm tra xem nếu ấn nút Shift từ bàn phím thì sẽ thay đổi speed của nhân vật
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8f;
        }
        else
            speed = 5f;
    }
    //chức năng của hàm này là nhận vào là tập tin InputManager.cs
    public  void ProcesMove(Vector2 input)
    {
        //tạo ra biến vector 3 để di chuyển cho đối tượng
        Vector3 moveDirection = Vector3.zero;
        //thay đổi hướng di chuyển của nhân vật được nhận từ input Vector 2 để phù hợp với 3D
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        //lấy giá trị nhân vào vào thay đổi cho biến nhân vật với biến speed và Time.deltaTime để chyaj với thời gian thực 
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVolocify.y += gravity * Time.deltaTime;
        //kiểm tra có được liên kết với mặt đất hay không và vạn tốc di chuyển phải nhỏ hơn hướng Y nhỏ hơn 0 
        if (IsGrounded && playerVolocify.y < 0) {
            playerVolocify.y = -2f;
        }
        controller.Move(playerVolocify * Time.deltaTime);
    }
    public void Jump()
    {
        //kiểm tra xem phayer có đnang chạm đất hay không
        if (IsGrounded)
        {
            playerVolocify.y = MathF.Sqrt(jumpHeight  * gravity * -3f);
        }
    }
}
