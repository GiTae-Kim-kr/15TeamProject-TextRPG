// 몬스터에 들어갈 기본 정보 
public class MonsterData
{
    public int level;
    public string name;
    public int hp;
    public int atk;

    // 생성자
    public MonsterData(int level, string name, int hp, int atk)
    {
        this.level = level;
        this.name = name;
        this.hp = hp;
        this.atk = atk;
    }
}

// 몬스터 종류 MonsterDB.monsterList[숫자]로 접근
public static class MonsterDB
{
    public static List<MonsterData> monsterList = new List<MonsterData>()
    {
        new MonsterData(2, "미니언", 15, 5),
        new MonsterData(3, "공허충", 10, 9),
        new MonsterData(5, "대포미니언", 25, 8)
    };
}

// 몬스터 - 인스턴스화 될 클래스
public class Monster
{
    public MonsterData data;
    public int hp;
    public bool isDead;

    // 생성자
    public Monster(MonsterData data)
    {
        this.data = data;
        this.hp = data.hp;
        this.isDead = false;    // 살아있는 상태로 시작
    }


    // 읽기전용 속성
    public int level => data.level;
    public string name => data.name;
    public int atk => data.atk;


}
