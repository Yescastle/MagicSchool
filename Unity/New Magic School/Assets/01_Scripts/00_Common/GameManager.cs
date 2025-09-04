using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤
    public static GameManager InstanceGM = null;
    private void Awake()
    {
        if (InstanceGM == null)
            InstanceGM = this;
    }

    [Header("전투 프리팹")]
    // 마법 프리팹 배열 생성
    public GameObject[] magics;

    // 마법진 프리팹 배열 생성
    public GameObject[] magicSquares;

    [Header("전투 시스템")]

    // 마법 생성 위치
    public Transform magicSpawnPoint;
    public Transform magicSquareZone;

    // 게이지 수치 변수
    float currentValue;

    // 게이지 최대 수치, 채우는 속도 변수
    public float maxGage;
    public float fillSpeed;

    // 플레이어 게이지 변수
    public Slider gageBar;

    // 마법 실행 UI 변수
    public GameObject magicsUI;

    // 마법 UI 애니메이션
    public Animator mAnim;

    // 마법 지속 변수
    public float show;

    // UI 켜져있는지 확인하기
    bool uiActive = false;

    // 버튼 하나가 눌리게 되면 다른 마법을 쓸 수 없다.
    bool inputLocked = false;

    // 마법의 공격력을 정한다. (시간 상 일단은 '5'씩 깎이는 걸로)
    public int attackPoint;

    // 마법 효과음
    public AudioSource[] attackSound;

    // 마법진 효과음
    public AudioSource msSound;

    [Header("플레이어 상태 관리")]
    // 플레이어 프리팹
    public GameObject[] players;

    // 플레이어 번호
    int slot;
    int pnum;

    // 현재 플레이어
    GameObject currentPlayer;

    // 플레이어 위치
    Vector2 pos;

    // 체력
    public int hP;

    // 최대 체력
    int maxHP = 30;

    // 체력바
    public Slider hPBar;

    // 플레이어 상태 상수
    public enum PlayerState
    {
        Idle,
        Attack,
        Damaged,
        Dead
    }

    // 플레이이 상태 변수
    public PlayerState pState;

    // 플레이어 애니메이터
    public Animator pAnim;

    // 엔딩메시지
    GameObject endingMessage;

    // 엔딩메시지 애니메이터
    Animator eAnim;

    // 전투 시작 시
    private void Start()
    {
        slot = PlayerPrefs.GetInt($"SelectedSlot", 1);
        pnum = PlayerPrefs.GetInt($"Slot{slot}_PlayerIndex", 0);

        endingMessage = EnemyManager.InstanceEM.endingMessage;
        eAnim = EnemyManager.InstanceEM.eAnim;

        // 플레이어 소환
        SpawnFight(pnum);

        pState = PlayerState.Idle;

        // 마법진 발동 위치는 플레이어의 위치 
        magicSquareZone = currentPlayer.transform;

        // hpBar는 플레이어 컴포넌트의 체력바
        hPBar = currentPlayer.GetComponentInChildren<Slider>();

        // P Anim은 소환된 플레이어의 애니메이션
        pAnim = currentPlayer.GetComponentInChildren<Animator>();

        // 게이지 채워지면 UI 호출
        SwitchOfUI();
    }

    private void Update()
    {
        GageConditionor();
    }

    // 플레이어 호출 메서드
    private void SpawnFight(int n)
    {
        // 저장된 슬롯의 번호를 가져온다.
        int slot = PlayerPrefs.GetInt($"SelectedSlot", n);

        // 저장된 캐릭터의 변수도 가져온다.
        int index = PlayerPrefs.GetInt($"Slot{n}_PlayerIndex", n);

        // 만약 인덱스 범위를 넘어서게 되면, 불꽃 캐릭터가 기본으로 나오게 된다.
        if (index < 0 || index > players.Length)
        {
            index = 0;
        }

        // 위치 지정하기
        pos = new Vector2(4f, -2f);

        currentPlayer = Instantiate(players[n], pos, Quaternion.identity);
    }

// 게이지 관리 메서드
private void GageConditionor()
    {
        // 만약 UI가 보이지 않는다?
        if (!uiActive && !inputLocked && EnemyManager.InstanceEM.eState != EnemyManager.EnemyState.Dead)
        {
            gageBar.gameObject.SetActive(true);

            // 게이지를 채워라
            currentValue += fillSpeed * Time.deltaTime;

            // 게이지가 최대로 채워졌을 때에는
            if (currentValue >= maxGage)
            {
                // 값을 최댓값으로 제한 하고 UI 활성화
                currentValue = maxGage;
                gageBar.gameObject.SetActive(false);
                ViewMagicUI();
            }
        }

        // UI 게이지 업데이트
        if (gageBar != null)
            gageBar.value = currentValue;
    }

    // UI 호출 메서드
    private void SwitchOfUI()
    {
        currentValue = 0;
        magicsUI.SetActive(false);
        gageBar.value = 0;
    }

    // UI 보이기 메서드
    private void ViewMagicUI()
    {
        // UI가 보여지게 된다.
        uiActive = true;
        mAnim.SetBool("ThisTime", true);

        // 만약 여기에 게임오브젝트가 들어가 있다면, 그 오브젝트는 활성화 되고, 쓸 수 있게 된다.
        if (magicsUI != null)
        {
            magicsUI.SetActive(true);
            ControlMagic(true);
        }

    }

    // 마법 호출 메서드
    public void OnMagic(int n)
    {
        // 이미 버튼 하나 눌렀으면 무시
        if (inputLocked) return;

        inputLocked = true;
        ControlMagic(false);

        magicsUI.SetActive(false);
        uiActive = false;

        StartCoroutine(ShowingMagic(n));

        EnemyManager.InstanceEM.HitEvent();
    }

    // 마법 시전 메서드
    IEnumerator ShowingMagic(int n)
    {
        // 공격 시전
        Attack();

        // 곧 바로 대기 상태로 변경
        Idle();

        // 마법진 시전
        GameObject magicSquare =
            Instantiate(magicSquares[n], new Vector3(magicSquareZone.position.x, -3.3f, magicSquareZone.position.z), Quaternion.Euler(300f, 0f, 0f));

        // 마법진 효과음
        AudioSource startMagic = msSound.GetComponent<AudioSource>();
        startMagic.Play();

        // 시간 지나면 해당 번호의 프리팹 소환
        yield return new WaitForSeconds(2f);
        GameObject magic = Instantiate(magics[n], magicSpawnPoint.position, Quaternion.identity);

        // 효과음도 같이 소환
        AudioSource magicSound = attackSound[n].GetComponent<AudioSource>();
        magicSound.Play();

        // 시간 지나면 마법 제거
        yield return new WaitForSeconds(show);

        Destroy(magic);
        Destroy(magicSquare);

        // 모든 게 끝나면 게이지 리셋
        SwitchOfUI();

        // 숨김 유지 및 다시 게이지 채우기
        HideMagic();

        // 여기서 잠시 2초 쉬어가기
        yield return new WaitForSeconds(1.5f);

        // 입력 해금
        inputLocked = false;
    }

    // UI 조작 메서드
    private void ControlMagic(bool intrct)
    {
        // 만약 게임오브젝트가 없다면 무시한다.
        if (magicsUI == null) return;

        // 해당 오브젝트의 자식 오브젝트들 중에서 버튼 컴포넌트를 가져오도록 한다.
        var btns = magicsUI.GetComponentsInChildren<Button>();

        // 그리고 그 버튼들을 제어한다.
        foreach (var b in btns)
            b.interactable = intrct;
    }

    // UI 숨김 메서드
    public void HideMagic()
    {
        uiActive = false;
        if (magicsUI != null)
        {
            ControlMagic(false);
            magicsUI.SetActive(false);
            mAnim.SetBool("ThisTime", false);
        }
    }

    // 플레이어 대기 상태
    public void Idle()
    {
        pState = PlayerState.Idle;
        pAnim.SetTrigger("ReturnIdle");
    }

    // 플레이어 공격 상태
    private void Attack()
    {
        // 상태 공격 상태로 변경
        pState = PlayerState.Attack;
        pAnim.SetTrigger("Attack");
    }

    // 플레이어 피격 상태
    public void Damaged()
    {
        pState = PlayerState.Damaged;
        pAnim.SetTrigger("Damaged");
    }
    public void HitEvent()
    {
        // 피격 연출
        StartCoroutine(DamageAction());
    }

    IEnumerator DamageAction()
    {
        // 상태가 Damaged로 바뀌며 피격 상태 연출.
        Damaged();

        // 체력을 깎는다.
        int damage = EnemyManager.InstanceEM.enemyDamage;

        // 체력이 깎이며 체력바도 같이 깎인다.
        hP -= damage;
        hPBar.value = (float)hP / maxHP;

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 애니메이션 끝나고 다시 대기 상태
        if (hP > 0)
        {
            Idle();
        }
        else if (hP <= 0)
        {
            Dead();

            yield return new WaitForSeconds(1f);

            currentPlayer.SetActive(false);
            endingMessage.SetActive(true);
            magicsUI.SetActive(false);
            eAnim.SetBool("ViewMessage", true);

            yield break;

        }
        print($"플레이어 체력: {hP}");
    }
    private void Dead()
    {
        hP = 0;
        pState = PlayerState.Dead;
        pAnim.SetTrigger("Dead");
        
    }
}