using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;

    public float maxHealth = 100f;
    private float chipSpeed = 3f;

    public GameObject itemPrefab;
    private float durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        if (health == 0)
        {
            Destroy(gameObject);
            SpawnItem();
        }
        Debug.Log(health.ToString());
       
    }

    public void TakeDamageEnemy(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
    }
    void SpawnItem()
    {// Lấy vị trí của boss
        Vector3 bossPosition = transform.position;

        // Tăng thêm 1 đơn vị vào trục y
        bossPosition.y += 1f;
        // Sinh ra prefab của vật phẩm tại vị trí của boss
        Instantiate(itemPrefab, bossPosition, Quaternion.identity);
    }
}
