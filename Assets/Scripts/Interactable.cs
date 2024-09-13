using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// lớp trừu tượng công khai để cho các class con kế thừa lại chức năng dễ dàng
public abstract class Interactable : MonoBehaviour

{
    //Sử dụng để các thành phần điều kiện của Events có thể Tương tác hay không
    public bool useEvents;
    //Sé được gọi khi player nhìn vào các vật thể có thể tương tác.
    [SerializeField] 
    public string promptMessga;
    
    // hàm này sẽ gọi lại (đệ quy) tới player
    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    } 
    protected virtual void Interact()
    {
        //Hàm này sẽ không có bất kỳ đoạn code nào;
        //Nó sẽ được ghi đè lên bười các lớp con của đối tượng.
    }
  
}
