// See https://aka.ms/new-console-template for more information
class Player
{
    public int level = 1;
    public string name;
    public string job;
    public int atk;
    public int def;
    public int maxHp;
    public int hp;
    public int maxMp;
    public int mp;
    public int gold;
    public int exp = 0;
    public int potionCount = 3;
    public int mpPotionCount = 2;

    private Random rand = new Random();

    
    // 싱글톤
    private static Player? instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
            }
            return instance;
        }
    }

    // 생성자
    public Player()
    {
        name = "르탄이";
        job = "전사";
        atk = 10;
        def = 5;
        maxHp = 100;
        hp = 100;           // 초기 체력
        maxMp = 100;
        mp = 50;            // 초기 마나
        gold = 15000;   // 초기 골드
    }

    // 필드 초기화 확인 변수
    private bool isInitialized = false;

    // 필드 초기화 메서드
    public void Init(string name, string job, int atk, int def, int maxHp, int maxMp, int gold)
    {
        if (isInitialized) return;  // 초기화 되어있으면, 덮어 쓰기 안함.

        this.name = name;
        this.job = job;
        this.atk = atk;
        this.def = def;
        this.maxHp = maxHp;
        this.hp = maxHp;
        this.maxMp = maxMp;
        this.mp = maxMp;
        this.gold = gold;

        isInitialized = true;
    }

    // 공격 - 데미지를반환
    public int Attack()
    {
        int variance = (int)Math.Ceiling(atk * 0.1f); // 분산
        int demage = rand.Next(atk - variance, atk + variance + 1);
        return demage;
    }
    
    // 치명타 - 치명 배율 반환
    public float Critical()
    {
        int num = rand.Next(1, 101);

        if (num <= 15) { return 1.6f; } // 15% 확률로 치명타
        else { return 1.0f; }
    }
}