using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        // ���� ���� ������ �ش� �ݶ��̴��� ������ �� ������ �������.
        if (other.CompareTag("Forest") || other.CompareTag("Lakeside") || other.CompareTag("Plain"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
