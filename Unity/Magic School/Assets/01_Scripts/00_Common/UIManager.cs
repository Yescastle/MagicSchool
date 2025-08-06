using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 싱글톤 객체 선언
    public static UIManager InstanceUI = null;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstanceUI == null)
            InstanceUI = this;
    }

    public void ToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ToSelectPlayer()
    {
        SceneManager.LoadScene("SelectCharacter");
    }

    public void ToGame()
    {
        SceneManager.LoadScene("World");
    }

    public void Setting()
    {

    }

    public void ExitGame()
    {

    }
}
