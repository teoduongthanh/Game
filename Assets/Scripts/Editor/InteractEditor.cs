using UnityEditor;

//đoạn code này nhằm khai báo Interactable là một Editor trong đối tượng được tương tác với Interactable
[CustomEditor(typeof(Interactable),true)]

//InteractEditor này sẽ được kề thừa từ Editor
public class InteractEditor : Editor
{
    //hàm nãy sẽ được gọi khi unity chập nhập giao diện chỉnh sửa
    public override void OnInspectorGUI()
    {
        //Interactable interactable = (Interactable)target;: Dòng này ép kiểu target thành kiểu Interactable
        //target là đối tượng mà Inspector đang hiển thị thông tin.
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessga = EditorGUILayout.TextField("Prompt Message", interactable.promptMessga);
            EditorGUILayout.HelpBox("EventInteractable can Only use Unityevent.", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (interactable.useEvents)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }

            else
            {
                // không thể sử dụng sự kiện, xóa component
                if (interactable.GetComponent<InteractionEvent>() != null)
                {
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());

                }
            }
        }
    }
}
