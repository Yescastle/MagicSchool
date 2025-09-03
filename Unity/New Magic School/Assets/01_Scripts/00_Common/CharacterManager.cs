using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // 싱글톤
    public static CharacterManager InstanceCM = null;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstanceCM == null)
            InstanceCM = this;
    }

    // 캐릭터 오브젝트
    public GameObject[] characters;

    // 엠블럼 오브젝트
    public GameObject[] emblems;

    // 초기에 보이는 캐릭터 >>> 캐릭터는 불꽃
    GameObject currentCharacter;

    // 초기에 보이는 엠블럼 >>> 불꽃
    GameObject currentEmblem;

    // 캐릭터를 저장할 슬롯의 번호
    public int currentIndex;

    private void Start()
    {
        // 초기에 보이는 캐릭터는 불꽃 캐릭터
        currentCharacter = characters[0];
        currentCharacter.SetActive(true);

        // 초기에 보이는 엠블럼은 불꽃 엠블럼
        currentEmblem = characters[0];
        currentEmblem.SetActive(true);

        // 나머지는 비활성화
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
            emblems[i].SetActive(false);
        }
    }

    public void viewCharacter(int n)
    {
        // 정보 저장 변수
        currentIndex = n;

        // 만약 그 버튼 누르면 그 버튼에 맞는 캐릭터와 엠블럼이 나온다.
        currentCharacter = characters[n];
        currentEmblem = emblems[n];

        // 만약 그 버튼의 캐릭터가 비활성화 상태라면 당장 활성화
        if (currentCharacter.activeSelf == false)
        {
            currentCharacter.SetActive(true);
            currentEmblem.SetActive(true);
        }

        // 반복문 돌려서 번호랑 안 맞으면 비활성화
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != n)
            {
                characters[i].SetActive(false);
                emblems[i].SetActive(false);
            }
        }

        // 각 슬롯에 저장된 값을 들고 온다.
        int slot = PlayerPrefs.GetInt("SelectedSlot", 1);

        // 플레이어 키값
        string key = $"Slot{slot}_PlayerIndex";
        PlayerPrefs.SetInt(key, n);
        PlayerPrefs.Save();

        Debug.Log($"[CharacterManager] viewCharacter {n}, Save to {key}, Value={n}");
    }
}
