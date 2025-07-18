using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

partial class BattleScene
{

    public void DroppedPotion()
    {
        Random random = new Random();
        int dromHPPotion = random.Next(0, 100);
        int dromMPPotion = random.Next(0, 100);
        
        if (dromHPPotion < 15)
        {
            droppedHPPotion++;
        }
        if (dromMPPotion < 10)
        {
            droppedMPPotion++;
        }
    }

    public void GetPotion()
    {
        player.potionCount += droppedHPPotion;
        player.mpPotionCount += droppedMPPotion;
        Console.WriteLine($"체력포션을 {droppedHPPotion} 개 획득하셨습니다.");
        Console.WriteLine($"마나포션을 {droppedMPPotion} 개 획득하셨습니다.");
        droppedHPPotion = 0;
        droppedMPPotion = 0;
    }
    public void DroppedItems(string name)
    {
        if (name=="미니언")
        {

        }
        else if (name=="공허충")
        {

        }
        else if(name=="대포미니언")
        {

        }
    }
    public void GetItems()
    {
        player.gold += droppedGold;
        Console.WriteLine($"{droppedGold}G 를 획득하셨습니다.");
        droppedGold = 0;
    }
}

