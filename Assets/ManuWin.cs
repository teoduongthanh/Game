using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ManuWin : MonoBehaviour
{
    private void Start()
    {
        // Khởi đầu, hiển thị chuột và không khóa chuột
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    //Thoát Game
    public void Quit()
    {
        Application.Quit();
    }
}
