using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 객체 선언
    public static GameManager InstanceGM = null;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstanceGM == null)
            InstanceGM = this;
    }

    // 프리팹 넣기
    public GameObject[] magics;
    public Transform magicSpawnPoint;

    // 마법 호출 메서드
    public void OnMagic(int n)
    {
       GameObject magic = Instantiate(magics[n], magicSpawnPoint.position, Quaternion.identity);

        Destroy(magic, 2f);
    }
}
