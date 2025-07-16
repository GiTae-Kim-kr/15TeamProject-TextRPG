using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal abstract class Skill
{
    public string name;    // 스킬이름
    public int consumeMp;  // 소비마나
    public string description; // 설명 

    public void Description()  // 스킬 설명
    {
        Console.WriteLine( $"{name} - MP {consumeMp}" );
        Console.WriteLine(description);
    }

    public abstract void Use();  // 스킬 사용 - 각 클래스에서 구현
}

class AlphaStrike : Skill
{
    public override void Use()
    {
        throw new NotImplementedException();
    }
}