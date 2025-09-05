using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    // 캔버스 목록
    public GameObject[] talkWindows;

    private void Start()
    {
        // 일단 캔버스 전체 비활성화
        foreach (var talkWindow in talkWindows)
        {
            talkWindow.gameObject.SetActive(false);
        }

        // 1.5초 기다리고 대화창 생성
        Invoke("SettingWindow", 1.5f);

        // 만약 교실이면 2초 더 기다리고 대화창 생성
        if (SceneManager.GetActiveScene().name == "Classroom") Invoke("Teacher", 3.5f); // 1.5초에 2초를 더 기다려야 하므로 3.5초로 설정
    }

    private void SettingWindow()
    {
        talkWindows[0].gameObject.SetActive(true);
    }

    private void Teacher()
    {
        talkWindows[1].gameObject.SetActive(true);
    }
}
