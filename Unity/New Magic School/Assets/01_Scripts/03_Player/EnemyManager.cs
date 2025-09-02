using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    // 싱글톤
    public static EnemyManager InstanceEM = null;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstanceEM == null)
            InstanceEM = this;
    }

    // 에너미 상태
    public enum EnemyState
    {
        Idle,
        Attack,
        Damaged,
        Dead
    }

    // 에너미 상태 변수
    public EnemyState eState;

    // 에너미의 애니메이터
    public Animator anim;

    // 에너미 체력
    public int enemyHP;

    // 에너미 최대 체력
    int maxHP = 20;

    // 에너미 게이지 변수
    public Slider enemyHPBar;

    // 전투 종료 메시지
    public GameObject endingMessage;
    public Animator eAnim;

    private void Start()
    {
        // 초기 에너미 상태: 대기
        eState = EnemyState.Idle;

        // 전투 종료 메시지 숨기기
        endingMessage.SetActive(false);
        StartCoroutine(Attack());
    }

    private void Idle()
    {
        eState = EnemyState.Idle;
        anim.SetTrigger("ReturnIdle");
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            // 죽지 않았을 때만 공격
            if (eState != EnemyState.Dead)
            {
                AttackState();
                GameManager.InstanceGM.Damaged();

                // 전부 대기 상태로 전환
                Idle();
                GameManager.InstanceGM.Idle();
            }
            else yield break;
        }
        
    }

    private void AttackState()
    {
        // 에너미 상태가 Attack으로 바뀌며 공격 상태 연출
        eState = EnemyState.Attack;
        anim.SetTrigger("EnemyAttack");
    }

    public void HitEvent()
    {
        // 피격 연출
        StartCoroutine(DamageAction());
    }

    IEnumerator DamageAction()
    {
        yield return new WaitForSeconds(2f);

        // 에너미 상태가 Damaged로 바뀌며 피격 상태 연출.
        eState = EnemyState.Damaged;
        anim.SetTrigger("EnemyDamaged");

        // 몬스터 체력을 깎는다.
        int damage = GameManager.InstanceGM.attackPoint;

        // 에너미 체력이 깎이며 체력바도 같이 깎인다.
        enemyHP -= damage;
        enemyHPBar.value = (float)enemyHP / maxHP;

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 애니메이션 끝나고 다시 대기 상태
        if (enemyHP > 0)
        {
            Idle();
        }
        else if (enemyHP <= 0)
        {
            Dead();

            yield return new WaitForSeconds(1f);

            gameObject.SetActive(false);
            endingMessage.SetActive(true);
            eAnim.SetBool("ViewMessage", true);

            StopCoroutine(DamageAction());

        }
        print($"몬스터 체력: {enemyHP}");
    }

    private void Dead()
    {
        enemyHP = 0;
        eState = EnemyState.Dead;

        anim.SetTrigger("EnemyDead");
    }
}


