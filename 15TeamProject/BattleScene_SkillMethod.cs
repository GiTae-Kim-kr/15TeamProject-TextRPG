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
        int num = 0;
        foreach (Skill skill in SkillDB.skillDB)
        {
            num++;
            Console.WriteLine($"{num}. ");
        }
    }

    void PlayerSkillPhase(Skill skill, List<Monster> monsters)
    {

    }

}
