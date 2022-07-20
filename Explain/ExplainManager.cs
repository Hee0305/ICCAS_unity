using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplainManager : MonoBehaviour
{
    /* Start ��ư�� Ŭ���Ǿ��� �� ����Ǵ� �Լ� */
    public void OnClickStartButton()
    {
        SceneManager.LoadScene((int)Definition.Scene.MiniGame);
    }

    /* Ŭ�� �Ҹ��� ���� �Լ� */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}