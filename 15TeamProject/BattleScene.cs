
using System.Diagnostics;

class BattleScene
{
    Player player = Player.Instance;

    public void Run()
    {
        Console.WriteLine("Battle!!\n\n");  // 한 칸 아래로 띄움
        RandomMonster();// 몬스터 랜덤으로 출력하는 코드
        Console.WriteLine("\n\n[내정보]");
        Console.WriteLine($"Lv.{player.level}  Chad ({player.job})");
        Console.WriteLine($"HP : {player.hp}/100\n");    // 한 칸 띄움
        Console.WriteLine("1. 공격\n");
        Console.Write("원하시는 행동을 입력해주세요. \n>>");
        string input = Console.ReadLine();  // 일단 아무거나 입력하면 PlayerPhase로 넘어감

        // 임시
        int target = 1; 
        PlayerPhase(target);
    }


    void PlayerPhase(int target)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!");
        Console.WriteLine();
        Console.ResetColor();

        Console.WriteLine($"{Player.name} 의 공격!");
        int demage = Player.Instance.Attack();
        // Console.WriteLine($"Lv.{monster[target].level} {monster[target].name} 을(를) 맞췄습니다. [데미지 : {}]");
    }

    void EnemyPhase()
    {
         
    }

    void ResultVictory()    // 전투 승리시 나오는 씬 메서드
    {
        Console.WriteLine("Battle!! - Result\n");
        Console.WriteLine("Victory\n");
        Console.WriteLine($"던전에서 몬스터 마리를 잡았습니다.\n");  // 몇 마리인지 표시하는 코드 추가 필요
        Console.WriteLine($"Lv.{player.level}  Chad ({player.job})");
        Console.WriteLine($"HP {player.hp} -> \n");  // 플레이어의 체력 표시 코드 추가 필요/ 아마 추가적인 hp 필드가 필요할 수도?
        Console.WriteLine($"0. 다음\n>>");
        string next = Console.ReadLine();

    }   

    void ResultLose()    // 전투 패배시 나오는 씬 메서드
    {
        Console.WriteLine("Battle!! - Result\n");
        Console.WriteLine("You Lose\n");
        Console.WriteLine($"Lv.{player.level}  Chad ({player.job})");
        Console.WriteLine($"HP {player.hp} -> 0 \n");  // 플레이어의 체력 표시 코드 추가 필요/ 아마 추가적인 hp 필드가 필요할 수도?
        Console.WriteLine($"0. 다음\n>>");
        string next = Console.ReadLine();
    }

    public void RandomMonster()   // 몬스터 랜덤으로 출력하는 메서드
    {
        Random random = new Random();
        
        int monsterNumber = random.Next(1, 5); // 1부터 4까지의 랜덤 숫자 생성
        Monster[] monsterInfo = new Monster[monsterNumber];    // 랜덤으로 뽑힌 몬스터 정보 저장 배열
        for (int i = 0; i < monsterNumber; i++)
        {
            int monsterIndex = random.Next(0,3); // 0부터 2까지의 랜덤 숫자 생성
            Monster monster = new Monster(MonsterDB.monsterList[monsterIndex]);
            monsterInfo[i] = monster;
            Console.WriteLine($"Lv.{monster.data.level} {monster.data.name}  HP {monster.hp}");
            //Console.WriteLine($"몬스터 {i + 1}: {monsterInfo[i].data.name}");  //잘 저장됬는지 디버깅용
        }

    }


}