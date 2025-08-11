using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCloud : MonoBehaviour
{
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
        if (gameObject.CompareTag("Forest")) SceneManager.LoadScene("Forest");
        if (gameObject.CompareTag("Plain")) SceneManager.LoadScene("Plain");
        if (gameObject.CompareTag("Lakeside")) SceneManager.LoadScene("Lakeside");
    }
}
