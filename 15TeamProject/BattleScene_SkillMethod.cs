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
        Console.WriteLine($"HP : {player.hp}/100\n");

        // 스킬 목록 출력
        int i = 0;
        foreach (Skill skill in SkillDB.skillDB)
        {
            i++;
            Console.Write($"{i}. ");
            skill.Describe();
        }

        // 입력 안내 출력
        Console.WriteLine("0. 취소\n");
        Console.WriteLine("원하시는 행동을 입력해주세요.");
        Console.Write(">>");

        // 입력 구분
        int input = Input.GetInt(0, SkillDB.skillDB.Count);
        if (input == 0)
        {            
            Run();
            return;            
        }
        else
        {
            Skill skill = SkillDB.skillDB[input - 1];
            List<Monster> monsters = new List<Monster>(monsterInfo);
            PlayerSkillPhase(skill, monsters);
            return;
        }
    }

    void PlayerSkillPhase(Skill skill, List<Monster> monsters)
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

        // 선택된 스킬 설명
        skill.Describe();
        Console.WriteLine();

        // 타겟팅
        if (skill.IsAutoTargeting)  // 자동 대상 지정이면
        {
            Console.WriteLine("자동으로 대상을 지정합니다.");
            List<Monster> aliveMonsters = new List<Monster>(monsterInfo);
            foreach (Monster monster in monsterInfo)
            {
                if (!monster.isDead)
                    aliveMonsters.Add(monster);
            }
            List<Monster> targetMonsters = skill.Targeting(aliveMonsters);
        }
        else
        {
            Console.WriteLine($"대상을 선택해주세요. (1 ~ {monsters.Count})\n>>");
            List<Monster> targetMonsters = skill.Targeting(monsters);
        }

        // 작성 중... 타겟팅한 몬스터에 공격 -> 몬스터 턴 진행으로 넘김
    }
}
