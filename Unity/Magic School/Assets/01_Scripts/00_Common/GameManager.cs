using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ��ü ����
    public static GameManager Instance = null;

    // �ڱ� �ڽ��� ������ �ڽ��� �Ҵ��Ѵ�.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
