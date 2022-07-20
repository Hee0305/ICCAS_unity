using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainManager : MonoBehaviour
{
    [Header("Static")]
    public static MainManager instance; //전역 참조 변수

    [Header("Update Text")]
    public int foodChoiceCount = Definition.foodChoiceCount; //음식 선택 가능 횟수
    public Text foodChoiceCountText; //Food Choice Count의 Text 컴포넌트
    public Text pointText; //Point의 Text 컴포넌트

    [Header("Food Button")]
    public GameObject sampleButton; //Sample Button 오브젝트
    public Transform contentTransform; //Content의 Transform 컴포넌트

    [Header("Point Alarm")]
    public GameObject pointAlarmObject;
    public Image pointAlarmImage;
    public Text pointAlarmText;
    private Color pointAlarmImageBasicColor; //이미지의 기본 색상
    private Color pointAlarmTextBasicColor; //텍스트의 기본 색상
    private Coroutine setPointAlarmCoroutine; //포인트 알람을 띄우는 코루틴

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetState();

        pointAlarmImageBasicColor = pointAlarmImage.color; //이미지의 기본 색상 저장
        pointAlarmTextBasicColor = pointAlarmText.color; //텍스트의 기본 색상 저장

        CreateFoodButtons();
    }

    /* 상태를 초기화하는 함수 */
    private void ResetState()
    {
        State.protein = 0f; //단백질 
        State.fat = 0f; //지방 
        State.carb = 0f; //탄수화물
    }

    private void Update()
    {
        UpdateFoodChoiceCountText();
        UpdatePointText();
    }

    #region Food Button
    /* Food Button들을 생성하는 함수 */
    private void CreateFoodButtons()
    {
        for (int i = 0; i < contentTransform.childCount; i++) //현재 생성되어있는 Food Button의 개수만큼 반복
        {
            if(contentTransform.GetChild(i).gameObject.activeSelf) Destroy(contentTransform.GetChild(i).gameObject); //Food Button 제거
        }

        int[] foods = Utility.GetRandomInt(Definition.displayFoodCount, 0, Definition.foodInfos.Length); //음식을 가져옴

        for (int i = 0; i < Definition.displayFoodCount; ++i) //표시되어야 하는 Food Button의 개수만큼 반복
        {
            GameObject button = Instantiate(sampleButton); //Food Buttons 생성
            button.GetComponent<FoodButton>().food = (Definition.Food)foods[i];//음식 정보 삽입
            button.transform.Find("FoodImage").GetComponent<Image>().sprite = Definition.foodInfos[foods[i]].sprite; //이미지 지정
            button.transform.Find("FoodName").GetComponent<Text>().text = Definition.foodInfos[foods[i]].engName; //이름 지정
            button.transform.Find("Point").GetComponent<Text>().text = "Point: " + Definition.foodInfos[foods[i]].point; //포인트 지정
            button.transform.SetParent(contentTransform, false); //부모 지정
            button.SetActive(true); //버튼 활성화
        }
    }

    /* Food Button을 클릭했을 때 실행되는 함수 */
    public void OnClickFoodButton(Definition.Food food)
    {
        if(Definition.foodInfos[(int)food].point > State.point) //포인트가 부족하면
        {
            if (setPointAlarmCoroutine != null) StopCoroutine(setPointAlarmCoroutine); //기존 포인트 알람 코루틴 종료
            setPointAlarmCoroutine = StartCoroutine(SetPointAlarm(Definition.pointAlarmDisplayTime)); //포인트 알람 실행
        }
        else //포인트가 충분하면
        {
            State.carb += Definition.foodInfos[(int)food].carb; //탄수화물 증가
            State.protein += Definition.foodInfos[(int)food].protein; //단백질 증가
            State.fat += Definition.foodInfos[(int)food].fat; //지방 증가

            State.point -= Definition.foodInfos[(int)food].point; //포인트 감소

            --foodChoiceCount; //음식 선택 가능 횟수 감소

            if(foodChoiceCount > 0) //아직 음식을 선택할 수 있으면
                CreateFoodButtons(); //새로운 Food Button 생성
            else //더 이상 음식을 선택할 수 없으면
                SceneManager.LoadScene((int)Definition.Scene.End); //씬 전환
        }
    }

    /* Skip Button을 클릭했을 때 실행되는 함수 */
    public void OnClickSkipButton()
    {
        --foodChoiceCount; //음식 선택 가능 횟수 감소

        if (foodChoiceCount > 0) //아직 음식을 선택할 수 있으면
            CreateFoodButtons(); //새로운 Food Button 생성
        else //더 이상 음식을 선택할 수 없으면
            SceneManager.LoadScene((int)Definition.Scene.End); //씬 전환
    }
    #endregion

    #region Point Alarm
    /* 포인트 알람을 띄우는 코루틴 함수 */
    public IEnumerator SetPointAlarm(float delay) // 지연 시간
    {
        /* 기본 색상으로 지정 */
        pointAlarmImage.color = pointAlarmImageBasicColor;
        pointAlarmText.color = pointAlarmTextBasicColor;

        pointAlarmObject.SetActive(true); //성실도 알람 활성화

        while (pointAlarmImage.color.a > 0f || pointAlarmText.color.a > 0f) // 하나라도 불투명한 상태이면
        {
            pointAlarmImage.color -= new Color(0, 0, 0, pointAlarmImageBasicColor.a) * Time.deltaTime / delay; //지연 시간에 맟추어 조금씩 투명도 증가
            pointAlarmText.color -= new Color(0, 0, 0, pointAlarmTextBasicColor.a) * Time.deltaTime / delay;

            yield return null; //이런 과정을 프레임 끝날 때 마다 반복
        }

        pointAlarmObject.SetActive(false); //성실도 알람 비활성화
    }
    #endregion

    #region Update Text
    /* Food Choice Count 텍스트를 갱신하는 함수 */
    private void UpdateFoodChoiceCountText()
    {
        foodChoiceCountText.text = "Choice(" + (Definition.foodChoiceCount - foodChoiceCount + 1).ToString() + "/" + Definition.foodChoiceCount + ")";
    }

    /* Point 텍스트를 갱신하는 함수 */
    private void UpdatePointText()
    {
        if (State.point < 0) //음수이면
            pointText.text = "Point: <color=#ff0000>" + State.point + "</color>"; //포인트 텍스트 갱신
        else //양수이면
            pointText.text = "Point: " + State.point; //포인트 텍스트 갱신
    }
    #endregion

    /* 클릭 소리를 내는 함수 */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}