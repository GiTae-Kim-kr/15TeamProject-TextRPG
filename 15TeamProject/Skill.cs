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

    public Skill(string name, int consumeMp, string description)
    {
        this.name = name;
        this.consumeMp = consumeMp;
        this.description = description;
    }

    public void Description()  // 스킬 설명
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
        new AlphaStrike("알파 스트라이크", 10, "공격력 * 2 로 하나의 적을 공격합니다."),
        new DoubleStrike("더블 스트라이크", 15, "공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.")

    };
}

class AlphaStrike : Skill
{
    // 생성자
    public AlphaStrike(string name, int consumeMp, string description) : base(name, consumeMp, description) { }

    public override List<Monster> Targeting(List<Monster> monsters)
    {
        while (true)
        {
            Console.WriteLine("타겟을 선택하세요. (1 ~ {0})", monsters.Count);
            int target = Input.GetInt();

            // 유효 범위 체크
            if (target >= 1 && target < monsters.Count)
            {
                return new List<Monster> { monsters[target - 1] };
            }
            else
            {
                Console.WriteLine("잘못된 번호입니다. 다시 입력해주세요.");
            }
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
    public DoubleStrike(string name, int consumeMp, string description) : base(name, consumeMp, description) { }

    Random rand = new Random();

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