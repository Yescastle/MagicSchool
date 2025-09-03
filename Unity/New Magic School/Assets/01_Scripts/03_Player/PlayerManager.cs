using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 캐릭터 스폰 위치
    // public Transform spawnPoint;

    // 캐릭터 프리팹
    public GameObject[] players;

    // 현재 캐릭터
    GameObject currentPlayer;

    // 캐릭터의 번호
    int playerN = CharacterManager.InstanceCM.currentIndex;

    private void Start()
    {
        SpawnPlayer(playerN);
    }

    private void SpawnPlayer(int n)
    {
        // 저장된 슬롯의 번호를 가져온다.
        int slot = PlayerPrefs.GetInt("SelectedSlot", n);

        // 저장된 캐릭터의 변수도 가져온다.
        int index = PlayerPrefs.GetInt($"Slot{n}_PlayerIndex", n);

        // 만약 인덱스 범위를 넘어서게 되면, 불꽃 캐릭터가 기본으로 나오게 된다.
        if (index < 0 || index > players.Length)
        {
            index = 0;
        }

        // 위치 불러오기
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        Vector2 pos = new Vector2(x, y);

        currentPlayer = Instantiate(players[n], pos, Quaternion.identity);


    }

}
