using UnityEngine;

public class DropObject : MonoBehaviour
{
    [Header("Component")]
    public GameObject dropObject; //떨어지는 물체 오브젝트
    public Rigidbody2D dropObjectRigidbody2D; //떨어지는 물체의 Rigidbody2D 컴포넌트

    [Header("Variable")]
    public int point; //획득하면 얻는 포인트 (음수도 가능)
    private float speed; //현재 속도
    private float setAcceleration; //설정된 가속도

    private void Start()
    {
        speed = Random.Range(Definition.miniGameDropObjectInitSpeed.x, Definition.miniGameDropObjectInitSpeed.y); //초기 속도 설정
        setAcceleration = Random.Range(Definition.miniGameDropObjectAcceleration.x, Definition.miniGameDropObjectAcceleration.y); //가속도 설정
        Destroy(dropObject, Definition.miniGameDropObjectLifeTime); //생존 시간 설정
    }

    private void Update()
    {
        SetSpeed();
    }

    private void FixedUpdate()
    {
        MoveDropObject();
    }

    /* 물체를 이동시키는 함수 */
    private void MoveDropObject()
    {
        dropObjectRigidbody2D.MovePosition(dropObjectRigidbody2D.position - speed * Time.fixedDeltaTime * Vector2.up); //속도에 따라 아래로 이동
    }

    /* 속도를 지정하는 함수 */
    private void SetSpeed()
    {
        speed += setAcceleration * Time.deltaTime; //가속도 적용
    }
}