using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MiniGameManager : MonoBehaviour
{
    [Header("Variable")]
    public float remainTime; //남은 시간

    [Header("Component")]
    public Text fullText; //Full Text의 Text 컴포넌트
    public Text remainTimeText; //Remain Time Text의 Text 컴포넌트
    public Text pointText; //Point Text의 Text 컴포넌트

    [Header("Drop Object")]
    public GameObject[] foods; //음식 오브젝트들
    public GameObject cigarette; //담배 오브젝트
    private Coroutine spawnDropObjectCoroutine; //SpawnDropObject 코루틴 함수

    private void Start()
    {
        ResetState();
        StartCoroutine(ProcessGame());
    }

    /* 상태를 초기화하는 함수 */
    private void ResetState()
    {
        State.point = 0; //포인트
    }

    private void Update()
    {
        UpdateRemainTimeText();
        UpdatePointText();
    }

    /* 게임을 진행하는 코루틴 함수 */
    private IEnumerator ProcessGame()
    {
        float currentStartDelay = Definition.miniGameStartDelay; //시작 지연 시간 초기화
        fullText.color = new Color(fullText.color.r, fullText.color.g, fullText.color.b, 1f); //투명도 초기화
        fullText.gameObject.SetActive(true); //Full Text 활성화
        pointText.gameObject.SetActive(false); //포인트 Text 비활성화

        while (currentStartDelay > 0f) //시작 지연 시간이 다 끝날때 까지 대기
        {
            yield return null;

            fullText.text = ((int)Mathf.Ceil(currentStartDelay)).ToString(); //남은 시간 출력 (올림)
            currentStartDelay -= Time.deltaTime; //지연 시간 감소
        }
        fullText.text = "Start"; //"Start" 출력

        remainTime = Definition.miniGamePlayTime; //남은 시간 지정
        pointText.gameObject.SetActive(true); //포인트 Text 활성화
        spawnDropObjectCoroutine = StartCoroutine(SpawnDropObject()); //떨어지는 물체 생성

        while (fullText.color.a > 0f) //투명하지 않으면
        {
            yield return null;

            fullText.color -= new Color(0f, 0f, 1f) * Time.deltaTime; //서서히 투명하게 변경
        }
        fullText.gameObject.SetActive(false); //Full Text 비활성화

        while (remainTime > 0f) yield return null; //미니 게임 진행 시간동안 대기

        StopCoroutine(spawnDropObjectCoroutine); //떨어지는 물체 생성 중지

        fullText.color = new Color(fullText.color.r, fullText.color.g, fullText.color.b, 1f); //투명도 복구
        fullText.gameObject.SetActive(true); //Full Text 활성화
        fullText.text = "Game Over"; //게임 종료 텍스트 출력


        yield return new WaitForSeconds(Definition.miniGameGameOverDelayTime); //잠시 대기
        SceneManager.LoadScene((int)Definition.Scene.Main); //씬 전환
    }

    /* 떨어지는 물체를 생성하는 코루틴 함수 */
    private IEnumerator SpawnDropObject()
    {
        while (true) //무한 반복
        {
            float delay = Random.Range(Definition.miniGameDropObjectSpawnDelayRange.x, Definition.miniGameDropObjectSpawnDelayRange.y); //생성할 때마다 생성 간격이 달라짐
            yield return new WaitForSeconds(delay); //생성 간격만큼 대기

            if (Utility.GetChance(Definition.miniGameCigaretteSpawnProbability)) //확률에 따라 담배를 생성해야 하면
            {
                GameObject clone = Instantiate(cigarette); //담배 오브젝트 복사
                clone.transform.position = new Vector3(Random.Range(Definition.miniGameDropObjectSpawnWidthRange.x, Definition.miniGameDropObjectSpawnWidthRange.y), Definition.miniGameDropObjectSpawnHeight, clone.transform.position.z); //위치 초기화
                clone.SetActive(true); //담배 오브젝트 활성화
            }
            else //확률에 따라 음식을 생성해야 하면
            {
                GameObject clone = Instantiate(foods[Random.Range(0, foods.Length)]); //음식 오브젝트 복사
                clone.transform.position = new Vector3(Random.Range(Definition.miniGameDropObjectSpawnWidthRange.x, Definition.miniGameDropObjectSpawnWidthRange.y), Definition.miniGameDropObjectSpawnHeight, clone.transform.position.z); //위치 초기화
                clone.SetActive(true); //음식 오브젝트 활성화
            }
        }
    }

    /* 남은 시간을 갱신하는 함수 */
    private void UpdateRemainTimeText()
    {
        if (remainTime > 0f) remainTime -= Time.deltaTime; //남은 시간 감소

        remainTimeText.gameObject.SetActive(remainTime > 0f); //남은 시간이 있으면 텍스트 표시

        remainTimeText.text = ((int)Mathf.Ceil(remainTime)).ToString(); //남은 시간 텍스트 갱신
    }

    /* 포인트를 갱신하는 함수 */
    private void UpdatePointText()
    {
        if(State.point < 0) //음수이면
            pointText.text = "Point: <color=#ff0000>" + State.point + "</color>"; //포인트 텍스트 갱신
        else //양수이면
            pointText.text = "Point: " + State.point; //포인트 텍스트 갱신
    }
}