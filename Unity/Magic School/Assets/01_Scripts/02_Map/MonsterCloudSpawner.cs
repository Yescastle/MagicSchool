using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCloudSpawner : MonoBehaviour
{
    // ���� ����
    public float xMin, xMax, yMin, yMax;

    // �ּ� �ð��� �ִ� �ð�
    float minTime = 0.5f;
    float maxTime = 2f;

    // ������ �ð����� ������ ����
    float currentTime;
    float createTime;

    // ���� ���� ����, �׸��� ������Ʈ Ǯ ����
    public GameObject selectedMonsterCloud;
    public List<GameObject> monsterCloudPool;
    public List<GameObject> respawnPool;

    // Ǯ ũ�� : �⺻�� 5
    public int poolSize;

    private void Start()
    {
        // ���� ���� �����ð��� �־��� ��������
        createTime = Random.Range(minTime, maxTime);

        // ������Ʈ Ǯ�� ������ �� �����.
        monsterCloudPool = new List<GameObject>();

        // Ǯ���ٰ� ���� ������ �ֱ�
        for (int i = 0; i < poolSize; i++)
        {
            // ���� ���� ��������
            GameObject monsterCloud = Instantiate(selectedMonsterCloud);

            // ����Ʈ���ٰ� ����ֱ�
            monsterCloudPool.Add(monsterCloud);

            // ��Ȱ��ȭ
            monsterCloud.SetActive(false);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > createTime)
        {
            if (monsterCloudPool.Count > 0)
            {
                // ����Ʈ���� ù��° ������ ����
                GameObject monsterCloud = monsterCloudPool[0];

                // �׸��� Ȱ��ȭ
                monsterCloud.SetActive(true);

                // ������ ���� ����
                monsterCloud.transform.position = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

                // ��Ÿ�� ������ ����Ʈ���� ����
                monsterCloudPool.RemoveAt(0);

                // ������ Ǯ�� ���ŵ� ������ ����ֱ�
                respawnPool.Add(monsterCloud);
            }

            // ���� �ð� 0���� �ʱ�ȭ
            currentTime = 0;

            // �� ���� �� �ð� �ʱ�ȭ
            createTime = Random.Range(minTime, maxTime);
        }

        if(respawnPool.Count > 0)
        {
            GameObject mc = respawnPool[0];
            if (mc.activeSelf == false)
            {
                monsterCloudPool.Add(mc);
                respawnPool.Remove(mc);
            }
        }
    }
}
