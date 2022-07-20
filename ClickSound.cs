using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [Header("Static")]
    public static ClickSound instance; //전역 클래스 => 현재 씬에 쿨릭 소리를 내는 AudioSource가 하나만 있어야 되기 때문에 그걸 확인하는 용

    [Header("Component")]
    public AudioSource clickAudioSource; //Click의 AudioSource 컴포넌트

    private void Awake()
    {
        if(instance) //이미 AudioSource가 있으면
        {
            Destroy(gameObject); //파괴
        }
        else //AudioSource가 없으면
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Scene 변경되어도 파괴하지 않는다
        }
    }

    /* 클릭 소리를 내는 함수 */
    public void PlayClickSound()
    {
        clickAudioSource.PlayOneShot(clickAudioSource.clip);
    }
}