using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // ���� �޴� ����â
    public Animator setting;

    public void SettingToggleKey(bool toggleKey)
    {
        setting.SetBool("IsHidden", toggleKey);
    }
}
