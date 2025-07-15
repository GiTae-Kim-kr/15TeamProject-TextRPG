// See https://aka.ms/new-console-template for more information
class Player
{
    public int level;
    public string name;
    public string job;
    public int atk;
    public int def;
    public int hp;
    public int gold;

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
        gold = 1500; // 초기 골드
    }

    public int Attack()
    {
        int variance = (int)Math.Ceiling(atk * 0.1f); // 분산
        int demage = rand.Next(atk - variance, atk + variance + 1);
        return demage;
    }
}