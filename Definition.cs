using UnityEngine;

public class Definition : MonoBehaviour
{
    public enum Scene //장면들 -> Build Setting에 있는 순서와 동일해야함
    {
        Title,
        Explain,
        MiniGame,
        Main,
        End
    }

    #region Mini Game
    public readonly static float miniGameStartDelay = 3f; //미니 게임 시작 지연 시간
    public readonly static float miniGamePlayTime = 60f; //미니 게임 진행 시간
    public readonly static float miniGameGameOverDelayTime = 3f; //미니 게임 종료 후 지연 시간
    public readonly static float miniGameDropObjectLifeTime = 5f; //미니 게임 떨어지는 물체의 생존 시간(ex> 5f = 5초 뒤에 파괴)
    public readonly static Vector2 miniGameDropObjectInitSpeed = new Vector2(1f, 2f); //미니 게임 떨어지는 물체의 초기 속도 (최소 속도, 최대 속도)
    public readonly static Vector2 miniGameDropObjectAcceleration = new Vector2(1f, 5f); //미니 게임 떨어지는 물체의 가속도 (최소 초당 가속도, 최대 초당 가속도)
    public readonly static Vector2 miniGameDropObjectSpawnWidthRange = new Vector2(-8f, 8f); //미니 게임 떨어지는 물체 가로 생성 범위
    public readonly static float miniGameDropObjectSpawnHeight = 5f; //미니 게임 떨어지는 물체 생성 높이
    public readonly static Vector2 miniGameDropObjectSpawnDelayRange = new Vector2(0f, 0.25f); //미니 게임 떨어지는 물체 생성 간격 (최소 간격, 최대 간격)
    public readonly static float miniGameCigaretteSpawnProbability = 50f; //미니 게임 담배 생성 확률
    public readonly static float miniGameBasketMoveSpeed = 7.5f; //미니 게임 바구니 이동 속도
    #endregion

    #region Food
    public static readonly int displayFoodCount = 10; //표시되는 음식의 개수
    public static readonly int foodChoiceCount = 5; //음식을 선택하는 횟수
    public static readonly float pointAlarmDisplayTime = 1f; //포인트 알람 표시 시간
    public struct FoodInfo //음식 정보
    {
        public string engName; //영어 이름
        public Sprite sprite; //이미지
        public float protein; // 단백질 
        public float fat; // 지방 
        public float carb; // 탄수화물
        public int point; //재료를 선택하는데 필요한 포인트

        public FoodInfo(string engName, float protein, float fat, float carb, int point) //생성자
        {
            this.engName = engName;
            sprite = null;
            this.protein = protein;
            this.fat = fat;
            this.carb = carb;
            this.point = point;
        }
    }

    public enum Food //음식을 정의하는 열거형
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

    public static string carbFoodRecommendation = "Your salad is low on [Carbohydrate] \n Try adding \n [Oatmeal, Banana, Sweet Potato]"; //탄수화물 음식 추천
    public static string proteinFoodRecommendation = "Your salad is low on [Protein] \n Try adding \n [Beef, Egg, Tofu, Chicken]"; //단백질 음식 추천
    public static string fatFoodRecommendation = "Your salad is low on [Fat] \n Try adding \n [Walnut, Almond, Pecan, Sunflower Seeds]"; //지방 음식 추천
    #endregion

    [Header("Sprites")]
    public Sprite[] foodSprites; //음식 이미지들

    private void Awake()
    {
        SetFoodSprites();
    }

    /* 음식의 이미지를 지정하는 함수 */
    private void SetFoodSprites()
    {
        for (int i = 0; i < foodInfos.Length; ++i) //음식의 개수만큼 반복
        {
            foodInfos[i].sprite = foodSprites[i]; //음식 이미지 지정
        }
    }
}