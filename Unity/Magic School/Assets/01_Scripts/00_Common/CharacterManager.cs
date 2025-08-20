using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // 캐릭터 오브젝트
    public GameObject[] characters;

    // 초기에 보이는 캐릭터 >>> 캐릭터는 불꽃
    GameObject currentCharacter;

    private void Start()
    {
        // 초기에 보이는 캐릭터는 불꽃 캐릭터
        currentCharacter = characters[0];
        currentCharacter.SetActive(true);

        // 나머지는 비활성화
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }

    public void viewCharacter(int n)
    {
        // 만약 그 버튼 누르면 그 버튼에 맞는 캐릭터가 나온다.
        currentCharacter = characters[n];

        // 만약 그 버튼의 캐릭터가 비활성화 상태라면 당장 활성화
        if (currentCharacter.activeSelf == false)
            currentCharacter.SetActive(true);

        // 반복문 돌려서 번호랑 안 맞으면 비활성화
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != n) characters[i].SetActive(false);
        }
    }
}
