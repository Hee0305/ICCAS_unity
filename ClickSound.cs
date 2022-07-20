using UnityEngine;

public class ClickSound : MonoBehaviour
{
    [Header("Static")]
    public static ClickSound instance; //���� Ŭ���� => ���� ���� �� �Ҹ��� ���� AudioSource�� �ϳ��� �־�� �Ǳ� ������ �װ� Ȯ���ϴ� ��

    [Header("Component")]
    public AudioSource clickAudioSource; //Click�� AudioSource ������Ʈ

    private void Awake()
    {
        if(instance) //�̹� AudioSource�� ������
        {
            Destroy(gameObject); //�ı�
        }
        else //AudioSource�� ������
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Scene ����Ǿ �ı����� �ʴ´�
        }
    }

    /* Ŭ�� �Ҹ��� ���� �Լ� */
    public void PlayClickSound()
    {
        clickAudioSource.PlayOneShot(clickAudioSource.clip);
    }
}