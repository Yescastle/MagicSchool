using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    private AudioSource audioSource;

    [System.Serializable]
    public class SceneBGM
    {
        public string sceneName;   // 씬 이름
        public AudioClip clip;     // 해당 씬에서 재생할 브금
    }

    public SceneBGM[] sceneBGMs;  // 씬별 브금 목록

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            // 씬 변경 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제 (메모리 누수 방지)
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 이름에 맞는 브금 찾기
        foreach (var sb in sceneBGMs)
        {
            if (sb.sceneName == scene.name)
            {
                PlayBGM(sb.clip);
                return;
            }
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
            return; // 같은 곡이면 무시

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
