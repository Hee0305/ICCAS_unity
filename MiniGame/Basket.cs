using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D basketRigidbody2D; //�ٱ����� Rigidbody2D ������Ʈ

    private void Update()
    {
        MoveBasket();
    }

    /* �ٱ��ϸ� �����̴� �Լ� */
    private void MoveBasket()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) //���� ����Ű�� Ŭ���ϸ�
        {
            basketRigidbody2D.position += Definition.miniGameBasketMoveSpeed * Time.deltaTime * Vector2.left; //�ٱ��� �������� �̵�
        }
        if (Input.GetKey(KeyCode.RightArrow)) //������ ����Ű�� Ŭ���ϸ�
        {
            basketRigidbody2D.position += Definition.miniGameBasketMoveSpeed * Time.deltaTime * Vector2.right; //�ٱ��� ���������� �̵�
        }
    }

    /* �ٱ��ϰ� �浹 �߻� �� ����Ǵ� �Լ� */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        State.point += collision.gameObject.GetComponent<DropObject>().point; //����Ʈ ����/�Ҹ�
        Destroy(collision.gameObject); //�浹�� ������Ʈ ����
    }
}