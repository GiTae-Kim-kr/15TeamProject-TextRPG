using _15TeamProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

partial class BattleScene
{
    void SkillSelect()
    {
        // 상단부는 이전 화면과 동일하게 출력
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
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
        Console.WriteLine($"MP : {player.mp}/50\n");

        // 스킬 목록 출력
        int i = 0;
        foreach (Skill skill in SkillDB.skillDB)
        {
            i++;
            if (player.mp < skill.ConsumeMp)    // 마나 부족 시 색깔 다르게 출력
                Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.Write($"{i}. ");
            skill.Describe();
            Console.ResetColor();
        }

        // 입력 안내 출력
        Console.WriteLine("\n0. 취소\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        // 입력 구분
        while (true)
        {
            int input = Input.GetInt(0, SkillDB.skillDB.Count);
            if (input == 0) // 0 입력시 뒤로
            {
                Run();
                return;
            }
            else
            {
                Skill skill = SkillDB.skillDB[input - 1];

                if (player.mp < skill.ConsumeMp)    // 마나가 부족할 때
                {
                    Console.WriteLine("마나가 부족합니다!");
                }
                else
                {
                    List<Monster> monsters = new List<Monster>(monsterInfo);
                    PlayerSkillPhase(skill, monsters);
                    return;
                }
            }
        }
    }

    void PlayerSkillPhase(Skill skill, List<Monster> monsters)
    {
        // 상단부: Battle! ~ 몬스터 목록 출력
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
        int i = 0;
        foreach (Monster monster in monsterInfo)
        {
            i++;

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

            Console.WriteLine($"{i}. Lv.{monster.data.level} {monster.data.name}  HP {afterHp}");
            Console.ResetColor();
        }

        // 선택된 스킬 설명
        Console.WriteLine();
        skill.Describe();
        Console.WriteLine();

        // 타겟팅
        List<Monster> targetMonsters = new List<Monster>();
        if (skill.IsAutoTargeting)  // 자동 대상 지정이면
        {
            Console.WriteLine("자동으로 대상을 지정합니다.");
            List<Monster> aliveMonsters = new List<Monster>();
            foreach (Monster monster in monsterInfo)
            {
                if (!monster.isDead)
                    aliveMonsters.Add(monster);
            }
            targetMonsters = skill.Targeting(aliveMonsters);
        }
        else
        {
            Console.Write($"대상을 선택해주세요.\n>>");
            targetMonsters = skill.Targeting(monsters);
        }

        // 입력 대기 
        Console.WriteLine("\n1. 확인");
        Console.Write("0. 취소\n\n>>");
        int input = Input.GetInt(0, 1);
        switch (input) 
        {
            case 0: SkillSelect(); return;
            case 1: PlayerAttackPhase(skill, targetMonsters); return;
        }       
    }

    void PlayerAttackPhase(Skill skill, List<Monster> targetMonsters)
    {
        // 화면 리셋
        Console.Clear();

        // 상단에 Battle 색 입혀서 출력, 스킬 사용 메시지 출력
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Battle!!\n");
        Console.ResetColor();
        Console.WriteLine($"{player.name} 의 {skill.Name}!\n");

        // 마나 소모 적용
        int beforeMp = player.mp;
        player.mp -= skill.ConsumeMp;
        Console.WriteLine($"Lv. {player.level} {player.name}");
        Console.WriteLine($"MP {beforeMp} -> {player.mp}\n");

        // 각 대상에 데미지 적용
        int enemyBeforeHp = 0;
        foreach (Monster monster in targetMonsters)
        {                           
            // 몬스터 hp 임시 저장
            enemyBeforeHp = monster.hp;

            // 데미지 적용
            int damage = player.atk;
            skill.CalculateDamage(out damage);
            monster.hp -= damage;

            // 적중 메시지
            Console.WriteLine($"Lv.{monster.level} {monster.name} 을(를) 맞췄습니다. [데미지 : {damage}] ");                       
        }

        // 각 대상 남은 hp 확인
        Console.WriteLine();
        foreach (Monster monster in targetMonsters)
        {
            // 적 사망 확인
            if (monster.hp <= 0)
            {
                monster.isDead = true;
                monster.hp = 0;    // 체력이 0이 되면 죽은 상태로 변경
                QuestConditioning.Instance.OnMonsterKilled(monster);   // 미니언 퀘스트
                DroppedPotion();
            }

            // 적 남은 hp 출력
            string afterHp = monster.isDead ? "Dead" : monster.hp.ToString();
            Console.WriteLine($"Lv.{monster.level} {monster.name}");
            Console.WriteLine($"HP {enemyBeforeHp} -> {afterHp}");
        }

        // 입력 대기 
        Console.WriteLine($"\n{player.name} 의 공격이 진행 중입니다.. \n(Enter키 입력 시 진행)");
        Console.ReadLine();
        EnemyPhase();
    }
}
