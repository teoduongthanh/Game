
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;

    [Header("Health Bar")]
    private float maxHealth =100f;
    private float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI textHealth;
    [Header("Health Overlay")]
    public Image overlay;
    public float duration;//thời gian image hiển thị hình ảnh màn hinh damage
    public float fadeSpeed;//tốc độ mờ đi của hình ảnh màn hinh damage
    private float durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
     
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestoreHealth(10);
        }
        textHealth.text = health.ToString() + "/100";
        //check xem Alpha của ảnh có > 0 để chạy code phía trong
        if(overlay.color.a > 0)
        {
            //nếu lượng hp giảm xuống quá 40 thì màn hình sẽ hiển thị liên tục
            if (health <= 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                //mỗi second updata frame sẽ giãm giá trị của overlay.color.a(độ trong suốt của hình)
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b,tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if(fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        if(health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);    
    }
    public void RestoreHealth(float healthAmount)
    {
        health += healthAmount;
        lerpTimer = 0f;
    }

}
