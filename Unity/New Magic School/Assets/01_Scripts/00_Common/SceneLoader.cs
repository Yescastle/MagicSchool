using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // 싱글톤 객체 선언
    public static SceneLoader InstanceSL = null;

    // 로딩 씬 적용 캔버스 지정
    public Image fadeImage;
    
    // 암전 시간
    public float fadeDuration;    // 1f;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstanceSL == null)
        {
            InstanceSL = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
            
    }

    public void ChangeScene(string sceneName)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("Scene 오브젝트 비활성화 상태");
            return;
        }
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        // 페이드 아웃
        yield return StartCoroutine(Fade(1f));

        // 씬 비동기 로딩
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        while(!async.isDone)
        {
            if(async.progress >= 0.9f) async.allowSceneActivation = true;

            yield return null;
        }

        // 페이드 인
        yield return StartCoroutine(Fade(0f));
    }

    // 페이드 코루틴 : 0은 밝아지고, 1은 어두워진다.
    IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float blend = time / fadeDuration;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(startAlpha, targetAlpha, blend);
            fadeImage.color = c;

            yield return null;
        }

        // 최종 보정
        Color final = fadeImage.color;
        final.a = targetAlpha;
        fadeImage.color = final;
    }
}
