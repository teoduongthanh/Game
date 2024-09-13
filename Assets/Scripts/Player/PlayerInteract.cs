using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//Class này sẽ chứa tất cả các logic có thể tương tác đến player  và xữ lý thông tin đầu vao(Input Manager)
public class PlayerInteract : MonoBehaviour
{
    //ham chiếu đến camera của player
    private Camera cam;
    [SerializeField]

    public float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        //khỏi tạo cho biến đối tượng cam đến camera hiển thị hình từ class player look 
        cam = GetComponent<PlayerLook>().camera;

        playerUI = GetComponent<PlayerUI>();

        inputManager = GetComponent<InputManager>();

    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        //Đối tượng Ray sẽ sử dụng các thuộc tính (Ray, hitinfo, maxDistance, layermask, queryTriggerInteraction)
        //Ray: điểm bắt đầu hướng tuyền tính của 1 vecter
        //hitinfo: nếu trên đường bắt đầu có một vật thể thì tuyền tính sẽ trã về thông tin "true"
        //Maxdistance: giới về độ dài của đường tuyến tính tương tác có thể va chạm
        //Layermask: xác định ray có thể thẻ tương tác với đối tượng nào khi va chạm với tuyến tính
        //queryTriggerInteraction:sẽ tương tác với các Collider trigger.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitinfo;
        // out hàm sẽ trả về một giá trij
        if (Physics.Raycast(ray, out hitinfo, distance, mask))
        {
            if(hitinfo.collider.GetComponent<Interactable>() != null)
            {
                //đoạn code sễ lấy giá trị khi ta nhấn vào nút E vào Keypad từ Inputmanager
                //đường Tuyến tính RayCast sẽ trả về giá trị để nhân biết
                Interactable interactable = hitinfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessga);
                if (inputManager.onFoot.Interact.triggered) 
                {
                    //Khi chúng ta ấn key E thì sẽ gọi tới hàm BaseInteract đồng thời chạy Interact trong đó
                    //Interact hàm này lại được liên kết với class Keypad và Overriding hàm này;
                    interactable.BaseInteract();
                }
            }
        }
    }
    
    
}
