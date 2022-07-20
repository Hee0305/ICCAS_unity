using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplainManager : MonoBehaviour
{
    /* Start 버튼이 클릭되었을 때 실행되는 함수 */
    public void OnClickStartButton()
    {
        SceneManager.LoadScene((int)Definition.Scene.MiniGame);
    }

    /* 클릭 소리를 내는 함수 */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}