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
        int addHp = 30;
        public void UsePotion()
        {
            if (player.potionCount > 0)
            {
                if (player.hp > 71)
                {
                    player.hp = 100;
                    player.potionCount--;
                    Console.WriteLine($"회복을 완료했습니다. 남은포션개수{player.potionCount}");
                }
                else if(player.hp==100)
                {
                    Console.WriteLine($"체력이 가득 찼습니다. 남은포션개수{player.potionCount}");
                }
                else
                {
                    player.hp += addHp;
                    player.potionCount--;
                    Console.WriteLine($"회복을 완료했습니다. 남은포션개수{player.potionCount}");
                }
            }
            else
            {
                Console.WriteLine("포션이 부족합니다.");
            }
        }
        public void GetPotion()
        {
            player.potionCount++;
        }
        public void ViewPotionInfo()
        {
            Console.WriteLine("회복");
            Console.WriteLine($"포션을 사용하면 체력을 {addHp} 회복 할 수 있습니다. (남은 포션 : {player.potionCount})");
            Console.WriteLine("");
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            int input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                UsePotion();
                Console.WriteLine();
                ViewPotionInfo();
            }
            else if (input == 0)
            {

            }
            else
            {
                Console.Clear();
                Console.WriteLine("원하시는 행동을 정확히 입력해주세요.");
                Console.WriteLine("");
                ViewPotionInfo();
            }
        }

    }
}
