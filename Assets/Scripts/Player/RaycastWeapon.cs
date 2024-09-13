using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RaycastWeapon : MonoBehaviour
{

    public bool isFiring = false;
    //tham chiếu đến hoạt ảnh tia lửa của nòng súng
    public ParticleSystem[] muzzleFlash;

    public ParticleSystem hitEffect;

    public TrailRenderer tracerEffect;

    public Transform rayCastOrigin;

    public Transform rayCastDestinetion;

    [Range(0.1f, 10f)]
    //tốc độ bắn bắn của boss
    public float fireRarePL = 3f;
    float shootTimerPL; 

    public AudioClip clickSound;  // Gán âm thanh click chuột vào đây
    private AudioSource audioSource;
    //Tạo ra hương đi của viên đạn bằng raycast 
    Ray ray;
    //để tính toán phàm vi của đường đạn dùng RaycastHit để tính nếu tia raycast sẽ
    //không có pham vi nhất định nó chỉ chạy cho tới khi đường raycast đó tiếp xúc với một vật thể
    // nhờ điểm tiếp xúc đó nó sẽ tính được pham vị của đường đạn và hướng đạn bắn.
    RaycastHit hitInfo;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void StartFiring()
    {
        isFiring = true;
        shootTimerPL = 0.0f;
        //sẽ phát hoạt ảnh khi ấn nút bắn
        FireBullet();

    }
    public void UpdateFiring(float deltaTime)
    {
        shootTimerPL += deltaTime;
        float fireInterval = 1.0f / fireRarePL;
        while (shootTimerPL >= 0.0f)
        {
            FireBullet();
            shootTimerPL -= fireInterval;

            PlayClickSound();
        }
    }

    private void FireBullet()
    {
        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }

        //vị trí viên đạn sẽ được sinh ra
        ray.origin = rayCastOrigin.position;
        //hướng viên đạn di chuyển
        ray.direction = rayCastDestinetion.position - rayCastOrigin.position;
        
        GameObject bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        GameObject bullet = GameObject.Instantiate(bulletPrefab, ray.origin, rayCastOrigin.transform.rotation);

      
        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);

        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 10f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;

            bullet.transform.position = hitInfo.point;
            bullet.transform.forward = hitInfo.normal;

            hitEffect.Emit(1);

            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring() 
    { 
      isFiring = false;
    }
    void PlayClickSound()
    {
        audioSource.volume = 0.4f;
        // Phát âm thanh click chuột từ AudioSource
        audioSource.PlayOneShot(clickSound);
    }
}
