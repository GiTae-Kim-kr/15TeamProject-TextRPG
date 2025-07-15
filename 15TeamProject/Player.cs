// See https://aka.ms/new-console-template for more information
class Player
{
    public int level;
    public string name;
    public string job;
    public int atk;
    public int def;
    public int hp;
    public int mp;
    public int gold;
    public int potionCount=3;

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

    private Player()
    {
        level = 1;
        name = "르탄이";
        job = "전사";
        atk = 10;
        def = 5;
        hp = 100; // 초기 체력
        mp = 50; // 초기 마나
        gold = 1500; // 초기 골드
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