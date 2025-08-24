using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCloudSpawner : MonoBehaviour
{
    // 범위 제한
    public float xMin, xMax, yMin, yMax;

    // 최소 시간과 최대 시간
    float minTime = 0.5f;
    float maxTime = 2f;

    // 정해진 시간동안 구름이 생성
    float currentTime;
    float createTime;

    // 몬스터 구름 선택, 그리고 오브젝트 풀 생성
    public GameObject selectedMonsterCloud;
    public List<GameObject> monsterCloudPool;
    public List<GameObject> respawnPool;

    // 풀 크기 : 기본은 5
    public int poolSize;

    private void Start()
    {
        // 몬스터 구름 생성시간은 주어진 범위에서
        createTime = Random.Range(minTime, maxTime);

        // 오브젝트 풀도 시작할 때 만든다.
        monsterCloudPool = new List<GameObject>();

        // 풀에다가 몬스터 구름들 넣기
        for (int i = 0; i < poolSize; i++)
        {
            // 몬스터 구름 가져오기
            GameObject monsterCloud = Instantiate(selectedMonsterCloud);

            // 리스트에다가 집어넣기
            monsterCloudPool.Add(monsterCloud);

            // 비활성화
            monsterCloud.SetActive(false);
        }
    }

    private void Update()
    {
        // 현재 시간 변수에 실제 흐르는 시간 더하기
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            if (monsterCloudPool.Count > 0)
            {
                // 리스트에서 첫번째 구름을 선택
                GameObject monsterCloud = monsterCloudPool[0];

                // 그리고 활성화
                monsterCloud.SetActive(true);

                // 범위는 랜덤 지정
                monsterCloud.transform.position = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

                // 나타난 구름은 리스트에서 제거
                monsterCloudPool.RemoveAt(0);

                // 리스폰 풀에 제거된 구름을 집어넣기
                respawnPool.Add(monsterCloud);
            }

            // 현재 시간 0으로 초기화
            currentTime = 0;

            // 적 생성 후 시간 초기화
            createTime = Random.Range(minTime, maxTime);
        }

        // 만약 리스폰 풀의 크기가 0보다 크다면?
        if(respawnPool.Count > 0)
        {
            // 일단 0을 넣어둘 변수를 하나 선언한다.
            int i = 0;

            // 그리고 그 변수가 리스폰 풀의 크기보다 작은 동안에
            while(i < respawnPool.Count)
            {
                // mc 게임오브젝트 변수에 리스폰 풀 내부의 오브젝트를 넣어두고
                GameObject mc = respawnPool[i];

                // 그 오브젝트가 비활성화 되어있다면
                if (mc.activeSelf == false)
                {
                    // 기존 주 오브젝트풀에 그 오브젝트를 보내고
                    monsterCloudPool.Add(mc);
                    // 리스폰 풀의 오브젝트는 제거한다.
                    respawnPool.Remove(mc);
                }
                // 만약 활성화 되어 있다면 i 값에 1씩 더한다.
                else i++;
            }
        }
    }
}
