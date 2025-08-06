using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // �̱��� ��ü ����
    public static UIManager InstanceUI = null;

    // �ڱ� �ڽ��� ������ �ڽ��� �Ҵ��Ѵ�.
    private void Awake()
    {
        if (InstanceUI == null)
            InstanceUI = this;
    }

    public void StartGame()
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
