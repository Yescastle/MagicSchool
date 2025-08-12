using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCloud : MonoBehaviour
{
    // 이동하는 속도 : 기본 5
    public float speed;

    // 방향 지정
    Vector2 dir;

    private void OnEnable()
    {
        // 활성화 되어 있는 동안 몇몇 구름은 플레이어 쪽으로 다가온다.
        int guided = Random.Range(0, 5);

        // 랜덤 값 중 3보다 작으면 그 구름은 플레이어 쪽으로 다가온다.
        if (guided < 3)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            dir = target.transform.position - transform.position;
            dir.Normalize();
        }
        // 안 그러면 그냥 중앙로 쪽으로 이동한다.
        else
        {
            // 숲은 중앙로보다 위에 있으므로 아래 방향으로 이동
            if (gameObject.CompareTag("Forest")) dir = Vector2.down;

            // 나머지는 중앙로보다 아래에 있으므로 위 방향으로 이동
            else dir = Vector2.up;
        }
        
    }

    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 플레이어와 맞닿는다면, 1.5초 있다가 전투씬으로 연결된다.
        if (other.CompareTag("Player"))
        {
            Invoke("ToFightScene", 1.5f);
            PlayerController.InstancePC.MeetOfMonsterCloud();
        }
    }

    private void ToFightScene()
    {
        // 만약 숲 몬스터 구름과 만났다면 숲 전투씬으로 이동된다.
        if (gameObject.CompareTag("Forest")) SceneManager.LoadScene("Forest");

        // 만약 평원 몬스터 구름과 만났다면 평원 전투씬으로 이동한다.
        if (gameObject.CompareTag("Plain")) SceneManager.LoadScene("Plain");

        // 만약 호숫가 몬스터 구름과 만났다면 호숫가 전투씬으로 이동한다.
        if (gameObject.CompareTag("Lakeside")) SceneManager.LoadScene("Lakeside");
    }
}
