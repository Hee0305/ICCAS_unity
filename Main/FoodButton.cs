using UnityEngine;

public class FoodButton : MonoBehaviour
{
    public Definition.Food food; //����

    /* Food Button�� Ŭ���Ǿ��� �� ����Ǵ� �Լ� */
    public void OnClickFoodButton()
    {
        MainManager.instance.OnClickFoodButton(food);
    }
}