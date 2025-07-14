// See https://aka.ms/new-console-template for more information
class Player
{
    public static int level;
    public static string name;
    public static string job;
    public static int atk;
    public static int def;
    public static int hp;
    public static int gold;


    
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