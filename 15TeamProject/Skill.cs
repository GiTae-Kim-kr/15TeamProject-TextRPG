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

    public Skill(string name, int consumeMp, string description)
    {
        this.name = name;
        this.consumeMp = consumeMp;
        this.description = description;
    }

    public void Description()  // 스킬 설명
    {
        Console.WriteLine( $"{name} - MP {consumeMp}" );
        Console.WriteLine(description);
    }

    public abstract void Use();  // 스킬 사용 - 각 클래스에서 구현
}

class SkillDB
{
    List<Skill> skillDB = new List<Skill>()
    {
        new AlphaStrike("알파 스트라이크", 10, "공격력 * 2 로 하나의 적을 공격합니다."),
        new DoubleStrike("더블 스트라이크", 15, "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.")
        
    };
}

class AlphaStrike : Skill
{
    // 생성자
    public AlphaStrike(string name, int consumeMp, string description) : base(name, consumeMp, description) { }

    public override void Use()
    {
        
    }
}

class DoubleStrike : Skill
{
    // 생성자
    public DoubleStrike(string name, int consumeMp, string description) : base (name, consumeMp, description) { }

    public override void Use()
    {

    }
}