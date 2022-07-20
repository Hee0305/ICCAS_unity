using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    [Header("State Bar")]
    public Image carbBar;
    public Text carbText;
    public Image proteinBar;
    public Text proteinText;
    public Image fatBar;
    public Text fatText;

    [Header("Detail")]
    public Text foodRecommendationText; //음식 추천 Text

    private void Start()
    {
        SetState();
        SetDetail();
    }

    /* State를 지정하는 함수 */
    private void SetState()
    {
        float maxState = Utility.GetHighestNumber(new float[] { State.carb, State.protein, State.fat });

        carbBar.fillAmount = State.carb / maxState; //탄수화물 바 지정
        proteinBar.fillAmount = State.protein / maxState; //단백질 바 지정
        fatBar.fillAmount = State.fat / maxState; //지방 바 지정

        carbText.text = string.Format("{0:N2}", State.carb); //탄수화물 텍스트 지정
        proteinText.text = string.Format("{0:N2}", State.protein); //단백질 텍스트 지정
        fatText.text = string.Format("{0:N2}", State.fat); //지방 텍스트 지정
    }

    /* 세부정보를 지정하는 함수 */
    private void SetDetail()
    {
        if (State.carb <= State.protein && State.carb <= State.fat) //탄수화물 음식 추천
            foodRecommendationText.text = Definition.carbFoodRecommendation;
        else if (State.protein <= State.carb && State.protein <= State.fat) //단백질 음식 추천
            foodRecommendationText.text = Definition.proteinFoodRecommendation;
        else //지방 음식 추천
            foodRecommendationText.text = Definition.fatFoodRecommendation;
    }

    /* Title Scene으로 넘어가는 함수 */
    public void LoadTitleScene()
    {
        SceneManager.LoadScene((int)Definition.Scene.Title);
    }

    /* 클릭 소리를 내는 함수 */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}