using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MiniGameManager : MonoBehaviour
{
    [Header("Variable")]
    public float remainTime; //���� �ð�

    [Header("Component")]
    public Text fullText; //Full Text�� Text ������Ʈ
    public Text remainTimeText; //Remain Time Text�� Text ������Ʈ
    public Text pointText; //Point Text�� Text ������Ʈ

    [Header("Drop Object")]
    public GameObject[] foods; //���� ������Ʈ��
    public GameObject cigarette; //��� ������Ʈ
    private Coroutine spawnDropObjectCoroutine; //SpawnDropObject �ڷ�ƾ �Լ�

    private void Start()
    {
        ResetState();
        StartCoroutine(ProcessGame());
    }

    /* ���¸� �ʱ�ȭ�ϴ� �Լ� */
    private void ResetState()
    {
        State.point = 0; //����Ʈ
    }

    private void Update()
    {
        UpdateRemainTimeText();
        UpdatePointText();
    }

    /* ������ �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator ProcessGame()
    {
        float currentStartDelay = Definition.miniGameStartDelay; //���� ���� �ð� �ʱ�ȭ
        fullText.color = new Color(fullText.color.r, fullText.color.g, fullText.color.b, 1f); //���� �ʱ�ȭ
        fullText.gameObject.SetActive(true); //Full Text Ȱ��ȭ
        pointText.gameObject.SetActive(false); //����Ʈ Text ��Ȱ��ȭ

        while (currentStartDelay > 0f) //���� ���� �ð��� �� ������ ���� ���
        {
            yield return null;

            fullText.text = ((int)Mathf.Ceil(currentStartDelay)).ToString(); //���� �ð� ��� (�ø�)
            currentStartDelay -= Time.deltaTime; //���� �ð� ����
        }
        fullText.text = "Start"; //"Start" ���

        remainTime = Definition.miniGamePlayTime; //���� �ð� ����
        pointText.gameObject.SetActive(true); //����Ʈ Text Ȱ��ȭ
        spawnDropObjectCoroutine = StartCoroutine(SpawnDropObject()); //�������� ��ü ����

        while (fullText.color.a > 0f) //�������� ������
        {
            yield return null;

            fullText.color -= new Color(0f, 0f, 1f) * Time.deltaTime; //������ �����ϰ� ����
        }
        fullText.gameObject.SetActive(false); //Full Text ��Ȱ��ȭ

        while (remainTime > 0f) yield return null; //�̴� ���� ���� �ð����� ���

        StopCoroutine(spawnDropObjectCoroutine); //�������� ��ü ���� ����

        fullText.color = new Color(fullText.color.r, fullText.color.g, fullText.color.b, 1f); //���� ����
        fullText.gameObject.SetActive(true); //Full Text Ȱ��ȭ
        fullText.text = "Game Over"; //���� ���� �ؽ�Ʈ ���


        yield return new WaitForSeconds(Definition.miniGameGameOverDelayTime); //��� ���
        SceneManager.LoadScene((int)Definition.Scene.Main); //�� ��ȯ
    }

    /* �������� ��ü�� �����ϴ� �ڷ�ƾ �Լ� */
    private IEnumerator SpawnDropObject()
    {
        while (true) //���� �ݺ�
        {
            float delay = Random.Range(Definition.miniGameDropObjectSpawnDelayRange.x, Definition.miniGameDropObjectSpawnDelayRange.y); //������ ������ ���� ������ �޶���
            yield return new WaitForSeconds(delay); //���� ���ݸ�ŭ ���

            if (Utility.GetChance(Definition.miniGameCigaretteSpawnProbability)) //Ȯ���� ���� ��踦 �����ؾ� �ϸ�
            {
                GameObject clone = Instantiate(cigarette); //��� ������Ʈ ����
                clone.transform.position = new Vector3(Random.Range(Definition.miniGameDropObjectSpawnWidthRange.x, Definition.miniGameDropObjectSpawnWidthRange.y), Definition.miniGameDropObjectSpawnHeight, clone.transform.position.z); //��ġ �ʱ�ȭ
                clone.SetActive(true); //��� ������Ʈ Ȱ��ȭ
            }
            else //Ȯ���� ���� ������ �����ؾ� �ϸ�
            {
                GameObject clone = Instantiate(foods[Random.Range(0, foods.Length)]); //���� ������Ʈ ����
                clone.transform.position = new Vector3(Random.Range(Definition.miniGameDropObjectSpawnWidthRange.x, Definition.miniGameDropObjectSpawnWidthRange.y), Definition.miniGameDropObjectSpawnHeight, clone.transform.position.z); //��ġ �ʱ�ȭ
                clone.SetActive(true); //���� ������Ʈ Ȱ��ȭ
            }
        }
    }

    /* ���� �ð��� �����ϴ� �Լ� */
    private void UpdateRemainTimeText()
    {
        if (remainTime > 0f) remainTime -= Time.deltaTime; //���� �ð� ����

        remainTimeText.gameObject.SetActive(remainTime > 0f); //���� �ð��� ������ �ؽ�Ʈ ǥ��

        remainTimeText.text = ((int)Mathf.Ceil(remainTime)).ToString(); //���� �ð� �ؽ�Ʈ ����
    }

    /* ����Ʈ�� �����ϴ� �Լ� */
    private void UpdatePointText()
    {
        if(State.point < 0) //�����̸�
            pointText.text = "Point: <color=#ff0000>" + State.point + "</color>"; //����Ʈ �ؽ�Ʈ ����
        else //����̸�
            pointText.text = "Point: " + State.point; //����Ʈ �ؽ�Ʈ ����
    }
}