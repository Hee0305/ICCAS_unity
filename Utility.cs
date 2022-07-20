using UnityEngine;

public class Utility
{
    /* 랜덤으로 정수를 원하는 만큼 뽑는 함수 */
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

    /* 기회를 얻는 함수 */
    public static bool GetChance(float percent)
    {
        if (percent >= 100f) return true;
        return (Random.value * 100f) < percent;
    }

    /* 가장 높은 숫자를 반환하는 함수 */
    public static float GetHighestNumber(float[] values)
    {
        float maxValue = float.MinValue; //최댓값
        for(int i = 0; i < values.Length; ++i) maxValue = values[i] > maxValue ? values[i] : maxValue; //가장 높은 값 탐색
        return maxValue; //가장 높은 값 반환
    }
}