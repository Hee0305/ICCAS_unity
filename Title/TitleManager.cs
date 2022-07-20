using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    /* Start Button이 클릭되었을 때 실행되는 함수 */
    public void OnClickStartButton() //public이면 외부 참조 가능
    {
        SceneManager.LoadScene((int)Definition.Scene.Explain);
    }

    /* Exit Button이 클릭되었을 때 실행되는 함수 */
    public void OnClickExitButton() //public이면 외부 참조 가능
    {
        Application.Quit();
    }

    /* 클릭 소리를 내는 함수 */
    public void PlayClickSound()
    {
        ClickSound.instance.PlayClickSound();
    }
}