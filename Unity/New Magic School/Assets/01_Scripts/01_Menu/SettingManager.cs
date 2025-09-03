using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // 시작 메뉴 설정창
    public Animator setting;

    public void SettingToggleKey(bool toggleKey)
    {
        setting.SetBool("IsHidden", toggleKey);
    }
}
