using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �̱��� ��ü ����
    public static GameManager InstanceGM = null;

    // �ڱ� �ڽ��� ������ �ڽ��� �Ҵ��Ѵ�.
    private void Awake()
    {
        if (InstanceGM == null)
            InstanceGM = this;
    }
}
