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
        // ���� �ð� ������ ���� �帣�� �ð� ���ϱ�
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

        // ���� ������ Ǯ�� ũ�Ⱑ 0���� ũ�ٸ�?
        if(respawnPool.Count > 0)
        {
            // �ϴ� 0�� �־�� ������ �ϳ� �����Ѵ�.
            int i = 0;

            // �׸��� �� ������ ������ Ǯ�� ũ�⺸�� ���� ���ȿ�
            while(i < respawnPool.Count)
            {
                // mc ���ӿ�����Ʈ ������ ������ Ǯ ������ ������Ʈ�� �־�ΰ�
                GameObject mc = respawnPool[i];

                // �� ������Ʈ�� ��Ȱ��ȭ �Ǿ��ִٸ�
                if (mc.activeSelf == false)
                {
                    // ���� �� ������ƮǮ�� �� ������Ʈ�� ������
                    monsterCloudPool.Add(mc);
                    // ������ Ǯ�� ������Ʈ�� �����Ѵ�.
                    respawnPool.Remove(mc);
                }
                // ���� Ȱ��ȭ �Ǿ� �ִٸ� i ���� 1�� ���Ѵ�.
                else i++;
            }
        }
    }
}
