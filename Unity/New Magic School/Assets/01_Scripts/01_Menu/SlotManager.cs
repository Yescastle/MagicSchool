using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [System.Serializable]
    public class SlotUI
    {
        public int slotNumber;          // 1, 2, 3
        public Image emblemImage;       // 슬롯 UI 이미지
        public Sprite defaultSprite;    // 빈 슬롯 스프라이트
    }

    public Sprite[] emblemSprites;      // 캐릭터별 엠블럼 스프라이트
    public SlotUI[] slots;              // 슬롯 UI들

    private void Start()
    {
        RefreshAllSlots();
    }

    public void OnSlot(int slot)
    {
        // 선택할 슬롯을 저장한다.
        PlayerPrefs.SetInt($"SelectedSlot", slot);
        PlayerPrefs.Save();

        // 해당 슬롯에 캐릭터 존재할 시 주어지는 키
        string key = $"Slot{slot}_PlayerIndex";
        Debug.Log($"[SlotManager] OnSlot {slot}, Key={key}, Exists={PlayerPrefs.HasKey(key)}, Value={PlayerPrefs.GetInt(key, -1)}");

        // 만약 캐릭터가 있다면, 바로 캠퍼스 씬으로 넘어가게 된다.
        if (PlayerPrefs.HasKey(key))
            SceneLoader.InstanceSL.ChangeScene("Campus");

        // 캐릭터가 없으면 바로 선택창으로 넘어간다.
        else
            SceneLoader.InstanceSL.ChangeScene("CreateNew");
    }

    public void RefreshAllSlots()
    {
        foreach (var slot in slots)
        {
            string key = $"Slot{slot.slotNumber}_PlayerIndex";

            if (PlayerPrefs.HasKey(key))
            {
                int index = PlayerPrefs.GetInt(key, 0);

                if (index >= 0 && index < emblemSprites.Length)
                    slot.emblemImage.sprite = emblemSprites[index];
                else
                    slot.emblemImage.sprite = slot.defaultSprite;
            }
            else
            {
                slot.emblemImage.sprite = slot.defaultSprite;
            }
        }
    }

    public void DeleteKeys(int n)
    {
        PlayerPrefs.DeleteKey($"Slot{n}_PlayerIndex");
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");

        slots[n - 1].emblemImage.sprite = slots[n - 1].defaultSprite;
    }
}
