using UnityEngine;

public class Definition : MonoBehaviour
{
    public enum Scene //���� -> Build Setting�� �ִ� ������ �����ؾ���
    {
        Title,
        Explain,
        MiniGame,
        Main,
        End
    }

    #region Mini Game
    public readonly static float miniGameStartDelay = 3f; //�̴� ���� ���� ���� �ð�
    public readonly static float miniGamePlayTime = 60f; //�̴� ���� ���� �ð�
    public readonly static float miniGameGameOverDelayTime = 3f; //�̴� ���� ���� �� ���� �ð�
    public readonly static float miniGameDropObjectLifeTime = 5f; //�̴� ���� �������� ��ü�� ���� �ð�(ex> 5f = 5�� �ڿ� �ı�)
    public readonly static Vector2 miniGameDropObjectInitSpeed = new Vector2(1f, 2f); //�̴� ���� �������� ��ü�� �ʱ� �ӵ� (�ּ� �ӵ�, �ִ� �ӵ�)
    public readonly static Vector2 miniGameDropObjectAcceleration = new Vector2(1f, 5f); //�̴� ���� �������� ��ü�� ���ӵ� (�ּ� �ʴ� ���ӵ�, �ִ� �ʴ� ���ӵ�)
    public readonly static Vector2 miniGameDropObjectSpawnWidthRange = new Vector2(-8f, 8f); //�̴� ���� �������� ��ü ���� ���� ����
    public readonly static float miniGameDropObjectSpawnHeight = 5f; //�̴� ���� �������� ��ü ���� ����
    public readonly static Vector2 miniGameDropObjectSpawnDelayRange = new Vector2(0f, 0.25f); //�̴� ���� �������� ��ü ���� ���� (�ּ� ����, �ִ� ����)
    public readonly static float miniGameCigaretteSpawnProbability = 50f; //�̴� ���� ��� ���� Ȯ��
    public readonly static float miniGameBasketMoveSpeed = 7.5f; //�̴� ���� �ٱ��� �̵� �ӵ�
    #endregion

    #region Food
    public static readonly int displayFoodCount = 10; //ǥ�õǴ� ������ ����
    public static readonly int foodChoiceCount = 5; //������ �����ϴ� Ƚ��
    public static readonly float pointAlarmDisplayTime = 1f; //����Ʈ �˶� ǥ�� �ð�
    public struct FoodInfo //���� ����
    {
        public string engName; //���� �̸�
        public Sprite sprite; //�̹���
        public float protein; // �ܹ��� 
        public float fat; // ���� 
        public float carb; // ź��ȭ��
        public int point; //��Ḧ �����ϴµ� �ʿ��� ����Ʈ

        public FoodInfo(string engName, float protein, float fat, float carb, int point) //������
        {
            this.engName = engName;
            sprite = null;
            this.protein = protein;
            this.fat = fat;
            this.carb = carb;
            this.point = point;
        }
    }

    public enum Food //������ �����ϴ� ������
    {
        cranberry,
        strawberry,
        blueberry,
        walnut,
        almond,
        peanut,
        cashew,
        pecan,
        chicken,
        beef,
        falafer,
        tofu,
        ceasar_sauce,
        balsamic_sauce,
        ltalian_dressing,
        chipotle_sauce,
        lettuce,
        cabbage
    }

    public static FoodInfo[] foodInfos =
    {
        // engName, float protein, float fat, float carb, int point 
        new FoodInfo("cranberry", 0, 0, 0.5f, 8),
        new FoodInfo("strawberry", 0, 0, 0.5f, 10),
        new FoodInfo("blueberry", 0, 0, 0.5f, 10),
        new FoodInfo("walnut", 0, 0.3f, 0.3f, 6),
        new FoodInfo("almond", 0, 0.5f, 0.2f, 8),
        new FoodInfo("peanut", 0, 0.5f, 0.3f, 8),
        new FoodInfo("cashew", 0, 0.5f, 0.2f, 6),
        new FoodInfo("pecan", 0, 0.3f, 0.3f, 6),
        new FoodInfo("chicken", 0.7f, 0, 0, 14),
        new FoodInfo("beef", 0.8f, 0, 0, 20),
        new FoodInfo("falafer", 0.5f, 0, 0, 16),
        new FoodInfo("tofu", 0.3f, 0, 0, 10),
        new FoodInfo("ceasar sauce", 0, 0.5f, 0, 4),
        new FoodInfo("balsamic sauce", 0, 0.5f, 0, 4),
        new FoodInfo("ltalian dressing", 0, 0.5f, 0, 4),
        new FoodInfo("chipotle sauce", 0, 0.5f, 0, 4),
        new FoodInfo("lettuce", 0, 0, 0.5f, 4),
        new FoodInfo("cabbage", 0, 0, 0.5f, 4)
    };

    public static string carbFoodRecommendation = "Your salad is low on [Carbohydrate] \n Try adding \n [Oatmeal, Banana, Sweet Potato]"; //ź��ȭ�� ���� ��õ
    public static string proteinFoodRecommendation = "Your salad is low on [Protein] \n Try adding \n [Beef, Egg, Tofu, Chicken]"; //�ܹ��� ���� ��õ
    public static string fatFoodRecommendation = "Your salad is low on [Fat] \n Try adding \n [Walnut, Almond, Pecan, Sunflower Seeds]"; //���� ���� ��õ
    #endregion

    [Header("Sprites")]
    public Sprite[] foodSprites; //���� �̹�����

    private void Awake()
    {
        SetFoodSprites();
    }

    /* ������ �̹����� �����ϴ� �Լ� */
    private void SetFoodSprites()
    {
        for (int i = 0; i < foodInfos.Length; ++i) //������ ������ŭ �ݺ�
        {
            foodInfos[i].sprite = foodSprites[i]; //���� �̹��� ����
        }
    }
}