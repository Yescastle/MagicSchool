using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �̱��� ��ü ����
    public static PlayerController InstancePC = null;

    // �ڱ� �ڽ��� ������ �ڽ��� �Ҵ��Ѵ�.
    private void Awake()
    {
        if (InstancePC == null)
            InstancePC = this;
    }

    // ���콺�� �̵��� ������ �̸�ŭ �̵��Ѵ�.
    public float moveForce;

    // ������ٵ� ������ ����
    private Rigidbody2D playrb;

    // ��ǥ ����
    public float xMin, xMax, yMin, yMax;

    // ��ǥ ��ǥ (���콺�� ���� ��ǥ)
    private Vector2? arrivePoint = null;

    // ���Ϳ� �ε��� ���� ����Ͽ�
    public bool canMove = true;

    private void Start()
    {
        // �÷��̾� ������ٵ� �� ��������
        playrb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ���� ���� false�� �Ǹ� �Ʒ� �ڵ�� ������� �ʴ´�.
        if (!canMove) return;

        // ���콺 ���� ��ư���� ����
        if (Input.GetMouseButton(0))
        {
            // ���콺�� Ŭ���� ���� ��ǥ ���ϱ�
            Vector2 mouseTakePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // �� ����Ʈ�� �÷��̾ �̵��� ��ġ�� ���ϱ�
            arrivePoint = mouseTakePos;
        }

        // ���� ������ �������ٸ�?
        if (arrivePoint != null)
        {
            // �ϴ� ���� �÷��̾ �ִ� ��ġ�� ���Ѵ�.
            Vector2 current = transform.position;

            // �÷��̾ ���� �������� ������ �ӵ�(moveForce)�� �̵��ϰ� �Ѵ�.
            Vector2 goingPoint = Vector2.MoveTowards(current, (Vector2)arrivePoint, moveForce * Time.deltaTime);

            // ������ ���¸� ����Ͽ�(��ǥ������ �÷��̾ ���� ������ ������ ��) ��ǥ�� �����Ѵ�.
            goingPoint = new Vector2(Mathf.Clamp(goingPoint.x, xMin, xMax), Mathf.Clamp(goingPoint.y, yMin, yMax));

            // �̵��ϴ� ���� �÷��̾��� ��ġ���� ����ȴ�.
            transform.position = goingPoint;

            // �̵� ��ġ���� ���� ������ �Ÿ��� 0.05���ϰ� �Ǹ� ���� ���� ��ǥ�� ������� �ʱ�ȭ �ȴ�.
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
