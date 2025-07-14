using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15TeamProject
{
    internal class HpPotion
    {
        int hpPotion = 3;
        int addHp=30;
        
        public void UsePotion()
        {
            Console.Clear();
            Console.WriteLine("회복");
            Console.WriteLine($"포션을 사용하면 체력을 {addHp} 회복 할 수 있습니다. (남은 포션 : {hpPotion})");
            Console.WriteLine("");
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.ReadLine();
        }

    }
}
