using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class AttackState : BaseState
{
    private float moveTimer;
    //khoảng thời gian kẽ địch ở trong trạng thái tấn công
    private float losePlayerTimer;
    private float shootTimer;
    public override void Enter()
    {
    }
    public override void Exit()
    {
    }
    public override void Perform()
    {
        if (enemy.CanSeePlayer())//check có nhìn thấy player 
        {
            // khóa bộ đếm thời gian của player và tăng bộ đếm thời khi bắn
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shootTimer += Time.deltaTime;
            //Giúp lấy vị trí của player và khiển cho Boss có góc nhìn vào player
            enemy.transform.LookAt(enemy.Player.transform);
            //nếu bộ đếm thời gian shootTimer > fireRate
            if(shootTimer > enemy.fireRare)
            {
                Shoot();
            }
            //khi boss ko nhìn thấy người chơi thì boss sẽ tự động di chuyển tới vị cuối cùng đã nhìn thấy player
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere) * 5);
                moveTimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position ;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                //thay đổi sang trạng thái tìm kiếm
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
    public void Shoot()
    {
       for(int i = 0; i < enemy.gunBarrel.Length;i++)
       {
            //tạo nơi lưu trữ vị trí và tham chiểu tới nòng súng
            Transform gunbarrel = enemy.gunBarrel[i];
            foreach (var particle in enemy.muzzleFlash)
            {
                particle.Emit(2);
            }
            //Sau khi tham chiếu để lấy được vị trí nòng súng 
            //sẽ khởi tạo Bullet từ prefab từ trong thư mục Resources (Resources có chức năng biến đổi các đối tượng thành một đường dẫn của đối tượng đó)
            GameObject bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
            GameObject bullet = GameObject.Instantiate(bulletPrefab, gunbarrel.position, enemy.transform.rotation);
            var tracer = GameObject.Instantiate(enemy.tracerEffect, gunbarrel.position, enemy.transform.rotation);
            enemy.PlayClickSound();
            //Xác định phương hướng cho đường đạn
            //(enemy.Player.transform.position - gunBarrel.transform.position) tính toán giữa vị trí player với nòng súng(shoot barrel) 
            //normalized được gọi để chắc chắn rằng vectơ hướng có độ dài bằng 1.
            //Điều này quan trọng để đảm bảo rằng hướng chỉ là hướng, không phải độ dài thực sự của vectơ.
            Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
            tracer.AddPosition(enemy.Player.transform.position);
            //thêm component rigigbody khi viên đạnh bán ra
            bullet.GetComponent<Rigidbody>().velocity =  shootDirection * 100;
            shootTimer = 0;
       }
    }
}
