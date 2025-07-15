
class BattleScene
{
    Player player = Player.Instance;

    public void Run()
    {
        Console.WriteLine("Battle!!\n");  // 한 칸 아래로 띄움
        RandomMonster();// 몬스터 랜덤으로 출력하는 코드
        Console.WriteLine("\n[내정보]");
        Console.WriteLine($"Lv.{player.level}  Chad ({player.job})");
        Console.WriteLine($"HP : {player.hp}/100\n");    // 한 칸 띄움
        Console.WriteLine("1. 공격\n");
        Console.Write("원하시는 행동을 입력해주세요. \n>>");
        string input = Console.ReadLine();


        PlayerPhase();
    }


    void PlayerPhase()
    {

    }

    void EnemyPhase()
    {
         
    }

    void ResultVictory()
    {

    }

    void ResultLose()
    {

    }

    public void RandomMonster()
    {
        Random random = new Random();
        int monsternumber = random.Next(1, 5); // 1부터 4까지의 랜덤 숫자 생성
        for (int i = 0; i < monsternumber; i++)
        {
            int monsterindex = random.Next(0,3); // 0부터 2까지의 랜덤 숫자 생성
            Monster monster = new Monster(MonsterDB.monsterList[monsterindex]);
            Console.WriteLine($"Lv.{monster.data.level} {monster.data.name}  HP {monster.data.hp}");
        }

    }


}