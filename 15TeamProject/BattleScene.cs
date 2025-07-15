
using System.Diagnostics;

class BattleScene
{
    Player player = Player.Instance;
    private Monster[] monsterInfo;

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
        EnemyPhase();
    }

    void EnemyPhase()
    {
        // 남은 적 확인
        int aliveCount = 0;
        foreach (Monster monster in monsterInfo)
        {
            if (!monster.isDead)    // 살아있는 몬스터 공격
            {
                // 남은 몬스터 수에 반영
                aliveCount++;

                // 화면 리셋
                Console.Clear();

                // 상단에 Battle 색 입혀서 출력
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                // 적 공격 메시지 출력
                Console.WriteLine($"Lv.{monster.level} {monster.name} 의 공격!");
                Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {monster.atk}]\n");

                // 플레이어 남은 체력 계산
                int beforeHp = player.hp;
                player.hp -= monster.atk;
                if (player.hp <= 0)     // 체력이 0이 되면 패배 (패배 화면으로 바로 이동)
                {
                    player.hp = 0;
                    ResultLose();
                    return;
                }

                // 플레이어 남은 체력 출력
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {beforeHp} -> {player.hp}\n");

                // 입력 대기
                Console.WriteLine("0. 다음\n");
                Console.WriteLine("대상을 선택해주세요.");
                Console.Write(">>");
                Console.ReadLine();
            }
        }
        
        // 전투 결과 판정
        if (aliveCount == 0)
        {
            // 몬스터가 모두 죽으면 종료 (승리 화면으로 이동)
            ResultVictory();
        }
        else
        {
            // 아직 적이 남아있으면 Run()으로 다시 이동
            Run();
        }
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