using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class HpPotion
    {
        Player player = Player.Instance;
        int potionCount = 3;
        int addHp = 30;
        public void UsePotion()
        {
            if (potionCount > 0)
            {
                if (player.hp > 71)
                {
                    player.hp = 100;
                    potionCount--;
                    Console.WriteLine("회복을 완료했습니다.");
                }
                else if(player.hp==100)
                {
                    Console.WriteLine("체력이 가득 찼습니다");
                }
                else
                {
                    player.hp += addHp;
                    potionCount--;
                    Console.WriteLine("회복을 완료했습니다.");
                }
            }
            else
            {
                Console.WriteLine("포션이 부족합니다.");
            }
        }
        public void GetPotion()
        {
            potionCount++;
        }
        public void ViewPotionInfo()
        {
            Console.Clear();
            Console.WriteLine("회복");
            Console.WriteLine($"포션을 사용하면 체력을 {addHp} 회복 할 수 있습니다. (남은 포션 : {potionCount})");
            Console.WriteLine("");
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.ReadLine();
        }

    }
}
