using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal abstract class Skill
{
    protected string name;    // 스킬이름
    protected int consumeMp;  // 소비마나
    protected string description; // 설명 
    protected bool isAutoTargeting; // 자동 타겟팅 여부

    // 읽기 전용
    public string Name => name;    
    public int ConsumeMp => consumeMp;  
    public string Description => description;
    public bool IsAutoTargeting => isAutoTargeting;

    public Skill(string name, int consumeMp, string description, bool isAutoTargeting)
    {
        this.name = name;
        this.consumeMp = consumeMp;
        this.description = description;
        this.isAutoTargeting = isAutoTargeting;
    }

    public void Describe()  // 스킬 설명
    {
        Console.WriteLine($"{name} - MP {consumeMp}");
        Console.WriteLine(description);
    }


    public abstract List<Monster> Targeting(List<Monster> monsters);
    public abstract void CalculateDamage(out int damage);  // 스킬 사용 - 각 클래스에서 구현
}

internal static class SkillDB
{
    public static List<Skill> skillDB = new List<Skill>()
    {
        new AlphaStrike("알파 스트라이크", 10, "공격력 * 2 로 하나의 적을 공격합니다.", false),
        new DoubleStrike("더블 스트라이크", 15, "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.", true),

    };
}

class AlphaStrike : Skill
{
    // 생성자
    public AlphaStrike(string name, int consumeMp, string description, bool isAutoTargeting) 
        : base(name, consumeMp, description, isAutoTargeting) { }

    // 대상 지정 메서드
    public override List<Monster> Targeting(List<Monster> monsters)
    {
        while (true)
        {
           int target = Input.GetInt(1, monsters.Count);

           return new List<Monster> { monsters[target - 1] };
        }
    }

    public override void CalculateDamage(out int damage)
    {
        damage = Player.Instance.atk * 2;
    }
}

class DoubleStrike : Skill
{
    // 생성자
    public DoubleStrike(string name, int consumeMp, string description, bool isAutoTargeting) 
        : base(name, consumeMp, description, isAutoTargeting) { }

    Random rand = new Random();

    // 대상 지정 메서드
    public override List<Monster> Targeting(List<Monster> monsters)
    {
        // 대상이 1마리 일 경우
        if (monsters.Count < 2)
        {            
            return new List<Monster>(monsters);
        }

        List<Monster> tempMonsters = new List<Monster> (monsters);
        Monster first = tempMonsters[rand.Next(0, tempMonsters.Count)];
        tempMonsters.Remove(first);
        Monster second = tempMonsters[rand.Next(0, tempMonsters.Count)];

        return new List<Monster> { first, second };
    }

    public override void CalculateDamage(out int damage)
    {
        damage = (int)(Player.Instance.atk * 1.5f);
    }
}