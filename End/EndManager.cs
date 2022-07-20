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
    public Text foodRecommendationText; //���� ��õ Text

    private void Start()
    {
        SetState();
        SetDetail();
    }

    /* State�� �����ϴ� �Լ� */
    private void SetState()
    {
        float maxState = Utility.GetHighestNumber(new float[] { State.carb, State.protein, State.fat });

        carbBar.fillAmount = State.carb / maxState; //ź��ȭ�� �� ����
        proteinBar.fillAmount = State.protein / maxState; //�ܹ��� �� ����
        fatBar.fillAmount = State.fat / maxState; //���� �� ����

        carbText.text = string.Format("{0:N2}", State.carb); //ź��ȭ�� �ؽ�Ʈ ����
        proteinText.text = string.Format("{0:N2}", State.protein); //�ܹ��� �ؽ�Ʈ ����
        fatText.text = string.Format("{0:N2}", State.fat); //���� �ؽ�Ʈ ����
    }

    /* ���������� �����ϴ� �Լ� */
    private void SetDetail()
    {
        if (State.carb <= State.protein && State.carb <= State.fat) //ź��ȭ�� ���� ��õ
            foodRecommendationText.text = Definition.carbFoodRecommendation;
        else if (State.protein <= State.carb && State.protein <= State.fat) //�ܹ��� ���� ��õ
            foodRecommendationText.text = Definition.proteinFoodRecommendation;
        else //���� ���� ��õ
            foodRecommendationText.text = Definition.fatFoodRecommendation;
    }

    /* Title Scene���� �Ѿ�� �Լ� */
    public void LoadTitleScene()
    {
        SceneManager.LoadScene((int)Definition.Scene.Title);
    }

    /* Ŭ�� �Ҹ��� ���� �Լ� */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}