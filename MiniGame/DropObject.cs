using UnityEngine;

public class DropObject : MonoBehaviour
{
    [Header("Component")]
    public GameObject dropObject; //�������� ��ü ������Ʈ
    public Rigidbody2D dropObjectRigidbody2D; //�������� ��ü�� Rigidbody2D ������Ʈ

    [Header("Variable")]
    public int point; //ȹ���ϸ� ��� ����Ʈ (������ ����)
    private float speed; //���� �ӵ�
    private float setAcceleration; //������ ���ӵ�

    private void Start()
    {
        speed = Random.Range(Definition.miniGameDropObjectInitSpeed.x, Definition.miniGameDropObjectInitSpeed.y); //�ʱ� �ӵ� ����
        setAcceleration = Random.Range(Definition.miniGameDropObjectAcceleration.x, Definition.miniGameDropObjectAcceleration.y); //���ӵ� ����
        Destroy(dropObject, Definition.miniGameDropObjectLifeTime); //���� �ð� ����
    }

    private void Update()
    {
        SetSpeed();
    }

    private void FixedUpdate()
    {
        MoveDropObject();
    }

    /* ��ü�� �̵���Ű�� �Լ� */
    private void MoveDropObject()
    {
        dropObjectRigidbody2D.MovePosition(dropObjectRigidbody2D.position - speed * Time.fixedDeltaTime * Vector2.up); //�ӵ��� ���� �Ʒ��� �̵�
    }

    /* �ӵ��� �����ϴ� �Լ� */
    private void SetSpeed()
    {
        speed += setAcceleration * Time.deltaTime; //���ӵ� ����
    }
}