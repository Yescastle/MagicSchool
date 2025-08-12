using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCloud : MonoBehaviour
{
    // �̵��ϴ� �ӵ� : �⺻ 5
    public float speed;

    // ���� ����
    Vector2 dir;

    private void OnEnable()
    {
        // Ȱ��ȭ �Ǿ� �ִ� ���� ��� ������ �÷��̾� ������ �ٰ��´�.
        int guided = Random.Range(0, 5);

        // ���� �� �� 3���� ������ �� ������ �÷��̾� ������ �ٰ��´�.
        if (guided < 3)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        // �� �׷��� �׳� �߾ӷ� ������ �̵��Ѵ�.
        else
        {
            // ���� �߾ӷκ��� ���� �����Ƿ� �Ʒ� �������� �̵�
            if (gameObject.CompareTag("Forest")) dir = Vector2.down;

            // �������� �߾ӷκ��� �Ʒ��� �����Ƿ� �� �������� �̵�
            else dir = Vector2.up;
        }
        
    }

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �÷��̾�� �´�´ٸ�, 1.5�� �ִٰ� ���������� ����ȴ�.
        if (other.CompareTag("Player"))
        {
            Invoke("ToFightScene", 1.5f);
            PlayerController.InstancePC.MeetOfMonsterCloud();
        }
    }

    private void ToFightScene()
    {
        // ���� �� ���� ������ �����ٸ� �� ���������� �̵��ȴ�.
        if (gameObject.CompareTag("Forest")) SceneManager.LoadScene("Forest");

        // ���� ��� ���� ������ �����ٸ� ��� ���������� �̵��Ѵ�.
        if (gameObject.CompareTag("Plain")) SceneManager.LoadScene("Plain");

        // ���� ȣ���� ���� ������ �����ٸ� ȣ���� ���������� �̵��Ѵ�.
        if (gameObject.CompareTag("Lakeside")) SceneManager.LoadScene("Lakeside");
    }
}
