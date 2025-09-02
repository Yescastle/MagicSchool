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

    // 게임 현재 상태
    public enum GameState
    {
        Play,
        Pause
    }

    // 메시지 박스 관련
    public GameObject updatemsg;

    // 시작/종료 관련 메서드
    public void ToStartMenu()
    {
        // 시작 메뉴로 이동
        SceneLoader.InstanceSL.ChangeScene("StartMenu");
        PlayerController.InstancePC.canMove = false;
    }

    public void ToCreateNew()
    {
        // 캐릭터 생성창으로 이동
        SceneLoader.InstanceSL.ChangeScene("CreateNew");
    }

    public void ToLoadInfo()
    {
        // 플레이어 선택창으로 이동
        SceneLoader.InstanceSL.ChangeScene("LoadInfo");
    }

    public void ToGame()
    {
        // 변경 예정
        SceneLoader.InstanceSL.ChangeScene("Campus");
    }


    // 맵 간 이동 메서드
    public void GoToSchool()
    {
        SceneLoader.InstanceSL.ChangeScene("School");
    }

    public void GoToCampus()
    {
        SceneLoader.InstanceSL.ChangeScene("Campus");
    }

    // 업데이트 예정 메시지
    public void ViewUpdateMessage()
    {
        StartCoroutine(ViewUpdateMessageTime());
    }

    IEnumerator ViewUpdateMessageTime()
    {
        if (updatemsg.activeSelf == false)
        {
            PlayerController.InstancePC.canMove = false;
            updatemsg.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        updatemsg.SetActive(false);
        PlayerController.InstancePC.canMove = true;
    }

    public void ExitGame()
    {
        // 조건: 실행되는 플랫폼이 유니티 에디터라면?
        #if UNITY_EDITOR
            // 유니티 상에서 종료
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 애플리케이션 종료
            Application.Quit();
        #endif
    }
}
