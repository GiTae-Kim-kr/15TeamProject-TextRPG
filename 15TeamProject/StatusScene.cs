using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class StatusScene
    {

        public void StatusViewScene()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Player.Instance.level}");
            Console.WriteLine($"{Player.Instance.name} ({Player.Instance.job})");
            Console.WriteLine($"공격력 : {Player.Instance.atk}");
            Console.WriteLine($"방어력 : {Player.Instance.def}");
            Console.WriteLine($"마  나 : {Player.Instance.mp} / {Player.Instance.maxMp}");
            Console.WriteLine($"체  력 : {Player.Instance.hp} / {Player.Instance.maxMp}");
            Console.WriteLine($"경험치 : {Player.Instance.exp}/{QuestList.NeedTotalExp(Player.Instance.level)}");
            Console.WriteLine($"Gold   : {Player.Instance.gold} G\n");

            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            while (true)
            {
                int choice = Input.GetInt(0, 1);
                switch (choice)
                {
                    case 0:
                        StartScene.Instance.GameStartScene();
                        return;
                    case 1:
                        Console.WriteLine("추후 추가");
                        break;
                }
            }
        }

    }
}
