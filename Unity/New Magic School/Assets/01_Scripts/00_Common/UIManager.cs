using System;
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
        if (SceneManager.GetActiveScene().name == "Campus" || SceneManager.GetActiveScene().name == "School")
            PlayerController.InstancePC.canMove = false;
    }

    public void ToCreateNew()
    {
        // 캐릭터 생성창으로 이동
        SceneLoader.InstanceSL.ChangeScene("CreateNew");
    }

    public void ToPlayerSlot()
    {
        // 플레이어 선택창으로 이동
        SceneLoader.InstanceSL.ChangeScene("PlayerSlot");
        if (SceneManager.GetActiveScene().name == "Campus" || SceneManager.GetActiveScene().name == "School")
        {
            PlayerController.InstancePC.canMove = false;

            // 현재 맵 저장
            PlayerPrefs.SetString("현재 맵 저장", $"{SceneManager.GetActiveScene().name}");

            // 현재 위치 저장
            PlayerPrefs.SetFloat("PlayerX", PlayerManager.InstancePM.currentPlayer.transform.position.x);
            PlayerPrefs.SetFloat("PlayerY", PlayerManager.InstancePM.currentPlayer.transform.position.y);

            PlayerPrefs.Save();
        }
    }

    public void ToGame()
    {
        if (PlayerPrefs.HasKey("현재 맵 저장"))
        {
            string scene = PlayerPrefs.GetString("현재 맵 저장");
            SceneLoader.InstanceSL.ChangeScene(scene);
        }
    }

    public void IntoClassroom()
    {
        // 교실로 이동
        SceneLoader.InstanceSL.ChangeScene("Classroom");

        PlayerPrefs.SetFloat("PlayerX", PlayerManager.InstancePM.currentPlayer.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerManager.InstancePM.currentPlayer.transform.position.y);
    }

    public void IntoLibrary()
    {
        // 도서관으로 이동
        SceneLoader.InstanceSL.ChangeScene("Library");

        PlayerPrefs.SetFloat("PlayerX", PlayerManager.InstancePM.currentPlayer.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerManager.InstancePM.currentPlayer.transform.position.y);
    }


    // 맵 간 이동 메서드
    public void GoToSchool()
    {
        SceneLoader.InstanceSL.ChangeScene("School");

        if (SceneManager.GetActiveScene().name == "Campus")
        {
            PlayerPrefs.DeleteKey("PlayerX");
            PlayerPrefs.DeleteKey("PlayerY");
        }
    }

    public void GoToCampus()
    {
        SceneLoader.InstanceSL.ChangeScene("Campus");
        if (SceneManager.GetActiveScene().name == "School")
        {
            PlayerPrefs.DeleteKey("PlayerX");
            PlayerPrefs.DeleteKey("PlayerY");
        }
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
