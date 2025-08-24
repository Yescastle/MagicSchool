using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // 싱글톤 객체 선언
    public static SceneLoader InstanceSL = null;

    // 로딩 씬 적용 캔버스 지정
    public CanvasGroup fadeImage;
    
    // 암전 시간
    public float fadeDuration;    // 2f;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstanceSL == null)
            InstanceSL = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene()
    {
        
    }
}
