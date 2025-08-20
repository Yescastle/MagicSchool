using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // ĳ���� ������Ʈ
    public GameObject[] characters;

    // �ʱ⿡ ���̴� ĳ���� >>> ĳ���ʹ� �Ҳ�
    GameObject currentCharacter;

    private void Start()
    {
        // �ʱ⿡ ���̴� ĳ���ʹ� �Ҳ� ĳ����
        currentCharacter = characters[0];
        currentCharacter.SetActive(true);

        // �������� ��Ȱ��ȭ
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
        }
    }

    public void viewCharacter(int n)
    {
        // ���� �� ��ư ������ �� ��ư�� �´� ĳ���Ͱ� ���´�.
        currentCharacter = characters[n];

        // ���� �� ��ư�� ĳ���Ͱ� ��Ȱ��ȭ ���¶�� ���� Ȱ��ȭ
        if (currentCharacter.activeSelf == false)
            currentCharacter.SetActive(true);

        // �ݺ��� ������ ��ȣ�� �� ������ ��Ȱ��ȭ
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != n) characters[i].SetActive(false);
        }
    }
}
