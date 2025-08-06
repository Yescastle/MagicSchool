using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 객체 선언
    public static GameManager Instance = null;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
