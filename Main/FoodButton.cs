using UnityEngine;

public class FoodButton : MonoBehaviour
{
    public Definition.Food food; //음식

    /* Food Button이 클릭되었을 때 실행되는 함수 */
    public void OnClickFoodButton()
    {
        MainManager.instance.OnClickFoodButton(food);
    }
}