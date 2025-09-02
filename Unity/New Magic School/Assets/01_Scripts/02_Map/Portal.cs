using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalUI;

    private void Start()
    {
        // UI를 연결시킨다.
        if (portalUI == null)
            portalUI = GetComponentInChildren<Canvas>(true)?.gameObject;

        // 처음에 UI는 보이지 않는다.
        portalUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            portalUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            portalUI.SetActive(false);
    }
}
