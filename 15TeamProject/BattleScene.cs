
using System.Diagnostics;

class BattleScene
{
    Player player = Player.Instance;
    private Monster[]? monsterInfo;       

    public void Run()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
        RandomMonster();// 몬스터 랜덤으로 출력하는 코드
        Console.WriteLine("\n\n[내정보]");
        Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
        Console.WriteLine($"HP : {player.hp}/100\n");    // 한 칸 띄움
        Console.WriteLine("1. 공격\n");
        Console.Write("원하시는 행동을 입력해주세요. \n>>");
        string input = Console.ReadLine();  // 일단 아무거나 입력하면 PlayerPhase로 넘어감

        if (input == "1") BattlePhase();  // 공격을 선택하면 BattlePhase로 넘어감
        else
        {
            Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
            Run();  // 1말고 다른거 입력하면 다시 Run() 메서드 호출 => 나중에 시작 화면으로 변경.
        }


    }

    void BattlePhase()
    {
        int count = 1;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
        foreach (Monster monster in monsterInfo)        // 랜덤으로 출력한 몬스터 정보 가져와서 다시 출력하기
        {
            Console.WriteLine($"{count} Lv.{monster.data.level} {monster.data.name}  HP {monster.hp}");
            count++;
        }
        Console.WriteLine("\n\n[내정보]");
        Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
        Console.WriteLine($"HP : {player.hp}/100\n");    // 한 칸 띄움
        Console.WriteLine("0. 취소\n");
        Console.Write("대상을 선택해주세요. \n>>");
        int target = int.Parse(Console.ReadLine());   // 몇 번 몬스터 맞출지.
        if (target == 0) Run();
        else PlayerPhase(target - 1);  // 0을 입력하면 취소로 작동되야해서 -1 해줌. 그 외의 숫자를 입력하면 PlayerPhase로 넘어감
    }


    void PlayerPhase(int target)
    {
        // 지정한 대상(적)을 저장
        Monster monster = monsterInfo[target];

        // 화면 리셋
        Console.Clear();    
        
        // 상단에 Battle 색 입혀서 출력
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();

        // 플레이어 공격 데미지 출력
        Console.WriteLine($"{player.name} 의 공격!");
        int demage = player.Attack();
        Console.WriteLine($"Lv.{monster.level} {monster.name} 을(를) 맞췄습니다. [데미지 : {demage}]\n");

        // 적 남은 hp 계산
        int beforeHp = monster.hp;
        monster.hp -= demage;
        if (monster.hp <= 0)
        {
            monster.isDead = true;
        }

        // 적 남은 hp 출력
        string afterHp = monster.isDead ? "Dead" : monster.hp.ToString();
        Console.WriteLine($"Lv.{monster.level} {monster.name}");
        Console.WriteLine($"HP {beforeHp} -> {afterHp}\n");

        // 입력 대기 - 판정 기능은 나중에 추가
        Console.WriteLine("0. 다음\n");
        Console.Write(">>");
        Console.ReadLine();
    }

    void EnemyPhase()
    {
         
    }

    void ResultVictory()    // 전투 승리시 나오는 씬 메서드
    {
        Console.WriteLine("Battle!! - Result\n");
        Console.WriteLine("Victory\n");
        

        Console.WriteLine($"던전에서 몬스터 {monsterInfo.Length}마리를 잡았습니다.\n");  // 몇 마리인지 표시하는 코드 추가 필요 => 어차피 생성된 모든 몬스터 잡아야 승리니까
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

        int monsterListIndex = MonsterDB.monsterList.Count; // 몬스터 리스트에 있는 몬스터 개수
        int monsterNumber = random.Next(1, 5); // 1부터 4까지의 랜덤 숫자 생성
        monsterInfo = new Monster[monsterNumber];    // 랜덤으로 뽑힌 몬스터 정보 저장 배열
        for (int i = 0; i < monsterNumber; i++)
        {
            int monsterIndex = random.Next(0,monsterListIndex); // 0부터 2까지의 랜덤 숫자 생성 -> 하드코딩했던거 수정했습니다.
            Monster monster = new Monster(MonsterDB.monsterList[monsterIndex]);
            monsterInfo[i] = monster;
            Console.WriteLine($"Lv.{monster.data.level} {monster.data.name}  HP {monster.hp}");

            //Console.WriteLine($"몬스터 {i + 1}: {monsterInfo[i].data.name}");  //잘 저장됬는지 디버깅용
        }

    }


}