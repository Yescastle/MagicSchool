using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCloud : MonoBehaviour
{
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
        if (gameObject.CompareTag("Forest")) SceneManager.LoadScene("Forest");
        if (gameObject.CompareTag("Plain")) SceneManager.LoadScene("Plain");
        if (gameObject.CompareTag("Lakeside")) SceneManager.LoadScene("Lakeside");
    }
}
