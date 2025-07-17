
using _15TeamProject;
using System;
using System.Diagnostics;

partial class BattleScene
{
    Player player = Player.Instance;
    private Monster[]? monsterInfo;       
    
    private int beforeHp;
    private int droppedHPPotion;
    private int droppedMPPotion;
    private int afterExp;     // 몬스터 처치하면 얻는 경험치 총 합.
    public void Run()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
        // 랜덤으로 출력한 몬스터 정보 가져와서 다시 출력하기
        foreach (Monster monster in monsterInfo)        
        {
            string afterHp;
            if (monster.isDead)
            {
                afterHp = "Dead";
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                afterHp = monster.hp.ToString();
            }

            Console.WriteLine($"Lv.{monster.data.level} {monster.data.name}  HP {afterHp}");
            Console.ResetColor();
        }
        Console.WriteLine("\n\n[내정보]");
        Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
        Console.WriteLine($"HP : {player.hp}/100");
        Console.WriteLine($"MP : {player.mp}/50\n");    // 한 칸 띄움
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 스킬 사용");
        Console.WriteLine("3. 포션 사용\n");
        Console.WriteLine("0. 나가기\n");
        Console.Write("원하시는 행동을 입력해주세요. \n>>");


        while (true)
        {
            int input = Input.GetInt();

            switch (input)
            {
                case 1:
                    BattlePhase();  // 공격을 선택하면 BattlePhase로 넘어감
                    return;
                case 2:
                    SkillSelect();
                    return;
                case 3:
                    Console.Clear();
                    Console.WriteLine();
                    HpPotion potion = new HpPotion();
                    potion.UsePotion();
                    Console.WriteLine();
                    Console.Write("아무키나 눌러 전투에 돌아가세요.");
                    Console.ReadLine();
                    Run();
                    return;
                case 0:
                    Console.Clear();
                    StartScene.Instance.GameStartScene();  // 시작 화면으로 돌아가기
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요."); // 잘못된 입력시 다시 전투 시작화면으로 돌아감
                    break;
            }

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
            string afterHp;
            if (monster.isDead)
            {
                afterHp = "Dead";
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else
            {
                afterHp = monster.hp.ToString();
            }

            Console.WriteLine($"{count} Lv.{monster.data.level} {monster.data.name}  HP {afterHp}");
            Console.ResetColor();
            count++;
        }
        Console.WriteLine("\n\n[내정보]");
        Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
        Console.WriteLine($"HP : {player.hp}/100");
        Console.WriteLine($"MP : {player.mp}/50\n");    // 한 칸 띄움
        Console.WriteLine("0. 취소\n");
        Console.Write("대상을 선택해주세요. \n>>");
        
        while (true)
        {
            int target = Input.GetInt(); ;   // 몇 번 몬스터 맞출지.
            if (target == 0)
            {
                Run();
                break;
            }            
            else if (target > monsterInfo.Length)
            {
                Console.WriteLine("생성되지 않은 몬스터를 선택하셨습니다! 다시 선택하여 주세요");
            }
            else if (target < 0)
            {
                Console.WriteLine("유효하지 않은 입력입니다. 다시 선택하여 주세요");
            }
            else if (monsterInfo[target - 1].isDead == true)
            {
                Console.WriteLine("이미 죽은 몬스터입니다! 다시 선택하여 주세요");
            }
            else
            {
                PlayerAttackPhase(target - 1);
                break;
            }
        }
    }

    void PlayerAttackPhase(int target)
    {
        // 지정한 대상(적)을 저장
        Monster monster = monsterInfo[target];
        int enemyBeforeHp = monster.hp;

        // 화면 리셋
        Console.Clear();    
        
        // 상단에 Battle 색 입혀서 출력, 공격 메시지 출력
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
        Console.WriteLine($"{player.name} 의 공격!");

        // 몬스터 회피 확인
        bool isMiss = monster.Dodge();
        if (isMiss) // 공격 회피 시
        {
            Console.WriteLine($"Lv.{monster.level} {monster.name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.\n");
        }
        else // 공격 적중 시
        {
            // 플레이어 공격 데미지 계산        
            int damage = player.Attack();
            float damageRatio = player.Critical();
            damage = (int)(damage * damageRatio);

            // 플레이어 공격 데미지 출력
            Console.Write($"Lv.{monster.level} {monster.name} 을(를) 맞췄습니다. [데미지 : {damage}] ");
            string isCritical;
            if (damageRatio != 1.0) // 치명타 여부 확인
            {
                isCritical = "- 치명타 공격 !!";
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                isCritical = "";
            }
            Console.WriteLine($"{isCritical}\n");
            Console.ResetColor();

            // 적 hp에 데미지 적용
            monster.hp -= damage;
        }                 

        // 적 사망 확인
        if (monster.hp <= 0)
        {
            monster.isDead = true;
            monster.hp = 0;    // 체력이 0이 되면 죽은 상태로 변경
            QuestConditioning.Instance.OnMonsterKilled(monster);   // 몬스터 처치 퀘스트
            afterExp += monster.exp;              // 처치된 몬스터 경험치 획득
            Console.WriteLine($"얻은 경험치 : {monster.exp}\n");
            DroppedPotion(); 
        }

        // 적 남은 hp 출력
        string afterHp = monster.isDead ? "Dead" : monster.hp.ToString();
        Console.WriteLine($"Lv.{monster.level} {monster.name}");
        Console.WriteLine($"HP {enemyBeforeHp} -> {afterHp}\n");

        // 입력 대기 
        Console.WriteLine($"{player.name} 의 공격이 진행 중입니다.. \n(Enter키 입력 시 진행)");
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
                beforeHp = player.hp;
                player.hp -= monster.atk;
                if (player.hp <= 0) player.hp = 0;  // 체력이 0 이하면 0으로 리셋                              

                // 플레이어 남은 체력 출력
                Console.WriteLine($"Lv.{player.level} {player.name}");
                Console.WriteLine($"HP {beforeHp} -> {player.hp}\n");

                // 입력 대기
                Console.WriteLine("적의 공격이 진행 중입니다.. \n(Enter키 입력 시 진행)");
                Console.ReadLine();

                // 체력이 0이 되면 패배 (패배 화면으로 바로 이동)
                if (player.hp <= 0)     
                {
                    ResultLose();
                    return;
                }
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
        Player.Instance.exp += afterExp;       // 승리 씬 나와야 경험치 획득
        bool isLevelUp;
        Console.Clear();
        // 상단에 Battle 색 입혀서 출력
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!! - Result\n");
        Console.ResetColor();
        Console.WriteLine("Victory\n");
        Console.WriteLine($"던전에서 몬스터 {monsterInfo.Length}마리를 잡았습니다.\n");  // 몇 마리인지 표시하는 코드 추가 필요 => 어차피 생성된 모든 몬스터 잡아야 승리니까
        GetPotion();
        Console.WriteLine("[캐릭터 정보]\n");
        Console.Write($"Lv.{player.level}  {player.name} ({player.job})");
        QuestList.Instance.LevelUp(out isLevelUp);
        if (isLevelUp) Console.WriteLine($" -> Lv.{player.level}  {player.name} ({player.job})");
        else Console.WriteLine();
        Console.WriteLine($"HP {StartScene.Instance.pastPlayerHP} -> {player.hp}");  // 플레이어의 체력 표시 코드 추가 필요/ 아마 추가적인 hp 필드가 필요할 수도?
        Console.WriteLine($"MP {StartScene.Instance.pastPlayerMP} -> {player.mp}");
        Console.WriteLine($"Exp {StartScene.Instance.pastPlayerExp} -> {StartScene.Instance.pastPlayerExp + afterExp}\n");
        Console.WriteLine("전투에서 승리했습니다! \n(Enter키 입력 시 진행)");
        Console.ReadLine();

        StartScene.Instance.GameStartScene();  // 시작 화면으로 돌아가기
    }   

    void ResultLose()    // 전투 패배시 나오는 씬 메서드
    {
        Console.Clear();
        // 상단에 Battle 색 입혀서 출력
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!! - Result\n");
        Console.ResetColor();
        Console.WriteLine("You Lose\n");
        Console.WriteLine($"Lv.{player.level}  {player.name} ({player.job})");
        Console.WriteLine($"HP {beforeHp} -> 0 \n");  // 플레이어의 체력 표시 코드 추가 필요/ 아마 추가적인 hp 필드가 필요할 수도?
        Console.WriteLine("전투에서 패배했습니다... \n(Enter키 입력 시 진행)");
        Console.ReadLine();

        StartScene.Instance.GameStartScene();  // 시작 화면으로 돌아가기
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
            //Console.WriteLine($"몬스터 {i + 1}: {monsterInfo[i].data.name}");  //잘 저장됬는지 디버깅용
        }

    }
     
       
}