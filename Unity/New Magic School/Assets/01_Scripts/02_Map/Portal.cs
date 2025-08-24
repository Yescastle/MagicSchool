using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalUI;

    private void Start()
    {
        // UI�� �����Ų��.
        if (portalUI == null)
            portalUI = GetComponentInChildren<Canvas>(true)?.gameObject;

        // ó���� UI�� ������ �ʴ´�.
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
