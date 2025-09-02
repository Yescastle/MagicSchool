using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        // 만약 몬스터 구름이 해당 콜라이더에 닿으면 그 구름은 사라진다.
        if (other.CompareTag("Forest") || other.CompareTag("Lakeside") || other.CompareTag("Plain"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
