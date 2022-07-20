using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainManager : MonoBehaviour
{
    [Header("Static")]
    public static MainManager instance; //���� ���� ����

    [Header("Update Text")]
    public int foodChoiceCount = Definition.foodChoiceCount; //���� ���� ���� Ƚ��
    public Text foodChoiceCountText; //Food Choice Count�� Text ������Ʈ
    public Text pointText; //Point�� Text ������Ʈ

    [Header("Food Button")]
    public GameObject sampleButton; //Sample Button ������Ʈ
    public Transform contentTransform; //Content�� Transform ������Ʈ

    [Header("Point Alarm")]
    public GameObject pointAlarmObject;
    public Image pointAlarmImage;
    public Text pointAlarmText;
    private Color pointAlarmImageBasicColor; //�̹����� �⺻ ����
    private Color pointAlarmTextBasicColor; //�ؽ�Ʈ�� �⺻ ����
    private Coroutine setPointAlarmCoroutine; //����Ʈ �˶��� ���� �ڷ�ƾ

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetState();

        pointAlarmImageBasicColor = pointAlarmImage.color; //�̹����� �⺻ ���� ����
        pointAlarmTextBasicColor = pointAlarmText.color; //�ؽ�Ʈ�� �⺻ ���� ����

        CreateFoodButtons();
    }

    /* ���¸� �ʱ�ȭ�ϴ� �Լ� */
    private void ResetState()
    {
        State.protein = 0f; //�ܹ��� 
        State.fat = 0f; //���� 
        State.carb = 0f; //ź��ȭ��
    }

    private void Update()
    {
        UpdateFoodChoiceCountText();
        UpdatePointText();
    }

    #region Food Button
    /* Food Button���� �����ϴ� �Լ� */
    private void CreateFoodButtons()
    {
        for (int i = 0; i < contentTransform.childCount; i++) //���� �����Ǿ��ִ� Food Button�� ������ŭ �ݺ�
        {
            if(contentTransform.GetChild(i).gameObject.activeSelf) Destroy(contentTransform.GetChild(i).gameObject); //Food Button ����
        }

        int[] foods = Utility.GetRandomInt(Definition.displayFoodCount, 0, Definition.foodInfos.Length); //������ ������

        for (int i = 0; i < Definition.displayFoodCount; ++i) //ǥ�õǾ�� �ϴ� Food Button�� ������ŭ �ݺ�
        {
            GameObject button = Instantiate(sampleButton); //Food Buttons ����
            button.GetComponent<FoodButton>().food = (Definition.Food)foods[i];//���� ���� ����
            button.transform.Find("FoodImage").GetComponent<Image>().sprite = Definition.foodInfos[foods[i]].sprite; //�̹��� ����
            button.transform.Find("FoodName").GetComponent<Text>().text = Definition.foodInfos[foods[i]].engName; //�̸� ����
            button.transform.Find("Point").GetComponent<Text>().text = "Point: " + Definition.foodInfos[foods[i]].point; //����Ʈ ����
            button.transform.SetParent(contentTransform, false); //�θ� ����
            button.SetActive(true); //��ư Ȱ��ȭ
        }
    }

    /* Food Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickFoodButton(Definition.Food food)
    {
        if(Definition.foodInfos[(int)food].point > State.point) //����Ʈ�� �����ϸ�
        {
            if (setPointAlarmCoroutine != null) StopCoroutine(setPointAlarmCoroutine); //���� ����Ʈ �˶� �ڷ�ƾ ����
            setPointAlarmCoroutine = StartCoroutine(SetPointAlarm(Definition.pointAlarmDisplayTime)); //����Ʈ �˶� ����
        }
        else //����Ʈ�� ����ϸ�
        {
            State.carb += Definition.foodInfos[(int)food].carb; //ź��ȭ�� ����
            State.protein += Definition.foodInfos[(int)food].protein; //�ܹ��� ����
            State.fat += Definition.foodInfos[(int)food].fat; //���� ����

            State.point -= Definition.foodInfos[(int)food].point; //����Ʈ ����

            --foodChoiceCount; //���� ���� ���� Ƚ�� ����

            if(foodChoiceCount > 0) //���� ������ ������ �� ������
                CreateFoodButtons(); //���ο� Food Button ����
            else //�� �̻� ������ ������ �� ������
                SceneManager.LoadScene((int)Definition.Scene.End); //�� ��ȯ
        }
    }

    /* Skip Button�� Ŭ������ �� ����Ǵ� �Լ� */
    public void OnClickSkipButton()
    {
        --foodChoiceCount; //���� ���� ���� Ƚ�� ����

        if (foodChoiceCount > 0) //���� ������ ������ �� ������
            CreateFoodButtons(); //���ο� Food Button ����
        else //�� �̻� ������ ������ �� ������
            SceneManager.LoadScene((int)Definition.Scene.End); //�� ��ȯ
    }
    #endregion

    #region Point Alarm
    /* ����Ʈ �˶��� ���� �ڷ�ƾ �Լ� */
    public IEnumerator SetPointAlarm(float delay) // ���� �ð�
    {
        /* �⺻ �������� ���� */
        pointAlarmImage.color = pointAlarmImageBasicColor;
        pointAlarmText.color = pointAlarmTextBasicColor;

        pointAlarmObject.SetActive(true); //���ǵ� �˶� Ȱ��ȭ

        while (pointAlarmImage.color.a > 0f || pointAlarmText.color.a > 0f) // �ϳ��� �������� �����̸�
        {
            pointAlarmImage.color -= new Color(0, 0, 0, pointAlarmImageBasicColor.a) * Time.deltaTime / delay; //���� �ð��� ���߾� ���ݾ� ���� ����
            pointAlarmText.color -= new Color(0, 0, 0, pointAlarmTextBasicColor.a) * Time.deltaTime / delay;

            yield return null; //�̷� ������ ������ ���� �� ���� �ݺ�
        }

        pointAlarmObject.SetActive(false); //���ǵ� �˶� ��Ȱ��ȭ
    }
    #endregion

    #region Update Text
    /* Food Choice Count �ؽ�Ʈ�� �����ϴ� �Լ� */
    private void UpdateFoodChoiceCountText()
    {
        foodChoiceCountText.text = "Choice(" + (Definition.foodChoiceCount - foodChoiceCount + 1).ToString() + "/" + Definition.foodChoiceCount + ")";
    }

    /* Point �ؽ�Ʈ�� �����ϴ� �Լ� */
    private void UpdatePointText()
    {
        if (State.point < 0) //�����̸�
            pointText.text = "Point: <color=#ff0000>" + State.point + "</color>"; //����Ʈ �ؽ�Ʈ ����
        else //����̸�
            pointText.text = "Point: " + State.point; //����Ʈ �ؽ�Ʈ ����
    }
    #endregion

    /* Ŭ�� �Ҹ��� ���� �Լ� */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}