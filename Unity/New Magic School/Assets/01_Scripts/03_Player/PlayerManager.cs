using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // 싱글톤
    public static PlayerManager InstancePM = null;
    private void Awake()
    {
        if (InstancePM == null)
            InstancePM = this;
    }

    // 캐릭터 위치
    Vector2 pos;

    // 캐릭터 프리팹
    public GameObject[] players;

    // 현재 캐릭터
    GameObject currentPlayer;

    // 캐릭터의 번호
    int currentIndex;

    // 카메라 변수
    public CinemachineVirtualCamera playerNavi;

    private void Start()
    {
        SpawnPlayer();

        playerNavi = GetComponent<CinemachineVirtualCamera>();
    }

    public void SpawnPlayer()
    {
        // 저장된 슬롯의 번호를 가져온다.
        int slot = PlayerPrefs.GetInt("SelectedSlot", 1);

        // 저장된 캐릭터의 변수도 가져온다.
        int index = PlayerPrefs.GetInt($"Slot{slot}_PlayerIndex", 0);

        // 만약 인덱스 범위를 넘어서게 되면, 불꽃 캐릭터가 기본으로 나오게 된다.
        if (index < 0 || index > players.Length)
        {
            index = 0;
        }

        // 위치 불러오기
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            pos = new Vector2(x, y);
        }
        else
            pos = new Vector2(0, -8f);

        currentPlayer = Instantiate(players[index], pos, Quaternion.identity);

        playerNavi.Follow = currentPlayer.transform;
    }

}
