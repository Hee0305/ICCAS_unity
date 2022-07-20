using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /* Start Button�� Ŭ���Ǿ��� �� ����Ǵ� �Լ� */
    public void OnClickStartButton() //public�̸� �ܺ� ���� ����
    {
        SceneManager.LoadScene((int)Definition.Scene.Explain);
    }

    /* Exit Button�� Ŭ���Ǿ��� �� ����Ǵ� �Լ� */
    public void OnClickExitButton() //public�̸� �ܺ� ���� ����
    {
        Application.Quit();
    }

    /* Ŭ�� �Ҹ��� ���� �Լ� */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}