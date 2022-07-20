using UnityEngine;

public class Utility
{
    /* �������� ������ ���ϴ� ��ŭ �̴� �Լ� */
    public static int[] GetRandomInt(int length, int min, int max)
    {
        int[] randArray = new int[length];
        bool isSame;

        for (int i=0; i<length; ++i)
        {
            while (true)
            {
                randArray[i] = Random.Range(min, max);
                isSame = false;

                for (int j=0; j<i; ++j)
                {
                    if (randArray[j] == randArray[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame) break;
            }
        }
        return randArray;
    }

    /* ��ȸ�� ��� �Լ� */
    public static bool GetChance(float percent)
    {
        if (percent >= 100f) return true;
        return (Random.value * 100f) < percent;
    }

    /* ���� ���� ���ڸ� ��ȯ�ϴ� �Լ� */
    public static float GetHighestNumber(float[] values)
    {
        float maxValue = float.MinValue; //�ִ�
        for(int i = 0; i < values.Length; ++i) maxValue = values[i] > maxValue ? values[i] : maxValue; //���� ���� �� Ž��
        return maxValue; //���� ���� �� ��ȯ
    }
}