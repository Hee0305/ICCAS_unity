using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Component")]
    public Rigidbody2D basketRigidbody2D; //바구니의 Rigidbody2D 컴포넌트

    private void Update()
    {
        MoveBasket();
    }

    /* 바구니를 움직이는 함수 */
    private void MoveBasket()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) //왼쪽 방향키를 클릭하면
        {
            basketRigidbody2D.position += Definition.miniGameBasketMoveSpeed * Time.deltaTime * Vector2.left; //바구니 왼쪽으로 이동
        }
        if (Input.GetKey(KeyCode.RightArrow)) //오른쪽 방향키를 클릭하면
        {
            basketRigidbody2D.position += Definition.miniGameBasketMoveSpeed * Time.deltaTime * Vector2.right; //바구니 오른쪽으로 이동
        }
    }

    /* 바구니가 충돌 발생 시 실행되는 함수 */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        State.point += collision.gameObject.GetComponent<DropObject>().point; //포인트 적립/소멸
        Destroy(collision.gameObject); //충돌한 오브젝트 제거
    }
}