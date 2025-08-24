using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 싱글톤 객체 선언
    public static PlayerController InstancePC = null;

    // 자기 자신이 없으면 자신을 할당한다.
    private void Awake()
    {
        if (InstancePC == null)
            InstancePC = this;
    }

    // 마우스로 이동할 때마다 이만큼 이동한다.
    public float moveForce;

    // 리지드바디 가져올 예정
    private Rigidbody2D playrb;

    // 좌표 제한
    public float xMin, xMax, yMin, yMax;

    // 목표 좌표 (마우스로 누른 좌표)
    private Vector2? arrivePoint = null;

    // 몬스터와 부딪힐 때를 대비하여
    public bool canMove = true;

    private void Start()
    {
        // 플레이어 리지드바디 값 가져오기
        playrb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 만약 값이 false가 되면 아래 코드는 실행되지 않는다.
        if (!canMove) return;

        // 마우스 왼쪽 버튼으로 조작
        if (Input.GetMouseButton(0))
        {
            // 마우스로 클릭한 곳의 좌표 구하기
            Vector2 mouseTakePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 그 포인트를 플레이어가 이동할 위치로 정하기
            arrivePoint = mouseTakePos;
        }

        // 도착 지점이 정해졌다면?
        if (arrivePoint != null)
        {
            // 일단 현재 플레이어가 있는 위치를 구한다.
            Vector2 current = transform.position;

            // 플레이어가 도착 지점까지 일정한 속도(moveForce)로 이동하게 한다.
            Vector2 goingPoint = Vector2.MoveTowards(current, (Vector2)arrivePoint, moveForce * Time.deltaTime);

            // 만일의 사태를 대비하여(대표적으로 플레이어가 범위 밖으로 나가는 거) 좌표를 제한한다.
            goingPoint = new Vector2(Mathf.Clamp(goingPoint.x, xMin, xMax), Mathf.Clamp(goingPoint.y, yMin, yMax));

            // 이동하는 값이 플레이어의 위치값에 저장된다.
            transform.position = goingPoint;

            // 이동 위치값과 도착 지점의 거리가 0.05이하가 되면 도착 지점 좌표가 사라지고 초기화 된다.
            if (Vector2.Distance(goingPoint, (Vector2)arrivePoint) < 0.05f)
                arrivePoint = null;
        }
    }

    public void MeetOfMonsterCloud()
    {
        canMove = false;
        playrb.velocity = Vector2.zero;
        playrb.angularVelocity = 0f;
    }

    public void ResumeMove()
    {
        canMove = true;
    }
}
