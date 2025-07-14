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


        
}