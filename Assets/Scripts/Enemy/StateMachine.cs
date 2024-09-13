using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //Lấy dữ liệu trạng thái từ class BaseState truyền vafoi biến activeState
    public BaseState activeState; 
    public void Initialise()
    {
        //Cài dặt trạng thái mặc định cho boss
        ChangeState(new PatrolState());

    }
    void Start()
    {
    }
    void Update()
    {
        activeState.Perform();
    }
    //Phương thức này dùng để thay đổi State của Machine(máy or boss)
    public void ChangeState(BaseState newState)
    {
        //kiểm tra hoạt động của trạng thái !=null
        if (activeState != null)
        {
            //chạy phương thức Exit()(là phương thức sẽ xóa đi trạng thái của Boss) trên trạng thái từ activeState
            activeState.Exit();
        }
        //thay đổi đến một trạng thái mới
        activeState = newState;
        //Kiểm tra xem activeState trạng thái có trạng thái hay không
        if(activeState != null)
        {
            //Set up trạng thái mới vào activeState
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            //Chỉ định lớp kẻ thù của trạng thái
            //sử dụng Enter()(để đẩy trạng thái Boss vào)
            activeState.Enter();
        }
    }
}
