using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    // 시작/종료 관련 메서드
    public void ToStartMenu()
    {
        // 시작 메뉴로 이동
        SceneManager.LoadScene("StartMenu");
    }
    public void ToSelectPlayer()
    {
        // 플레이어 선택창으로 이동
        SceneManager.LoadScene("SelectCharacter");
    }
    public void ToCreateNew()
    {
        // 플레이어 생성창으로 이동
        SceneManager.LoadScene("CreateNew");
    }
    public void ToGame()
    {
        // (변경 예정): 현재 본 메서드는 "World" 씬으로 연결되도록 설정.
        SceneManager.LoadScene("World");
    }
    public void ExitGame()
    {
        // 빌드 상에서 게임 종료
        Application.Quit();

        // 유니티 에디터 상에서 플레이 모드 종료
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // 맵 간 이동 메서드
    public void GoToSchool()
    {
        // 학교 맵으로 이동
        SceneManager.LoadScene("School");
    }
    public void GoToMarketplace()
    {
        // 상점가 맵으로 이동
        SceneManager.LoadScene("Marketplace");
    }
    public void GoToVillage()
    {
        // 마을 맵으로 이동
        SceneManager.LoadScene("Village");
    }
    public void GoToWorld()
    {
        // 월드 맵으로 이동
        SceneManager.LoadScene("World");
    }

}
