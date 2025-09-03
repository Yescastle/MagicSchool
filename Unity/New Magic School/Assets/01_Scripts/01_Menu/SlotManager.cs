using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public void OnSlot(int n)
    {
        // 선택할 슬롯을 저장한다.
        PlayerPrefs.SetInt("SelectedSlot", n);

        // 해당 슬롯에 캐릭터 존재할 시 주어지는 키
        string key = $"Slot{n}_PlayerIndex";

        // 만약 캐릭터가 있다면, 바로 캠퍼스 씬으로 넘어가게 된다.
        if (PlayerPrefs.HasKey(key))
            SceneLoader.InstanceSL.ChangeScene("Campus");

        // 캐릭터가 없으면 바로 선택창으로 넘어간다.
        else
            SceneLoader.InstanceSL.ChangeScene("CreateNew");
    }
}
