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

    // 복사 생성자
    public MonsterData(MonsterData other)
    {
        name = other.name;
        level = other.level;
        hp = other.hp;
        atk = other.atk;
    }
}

// 몬스터 종류 MonsterDB.monsterList[숫자]로 접근
public static class MonsterDB
{
    // 몬스터 난이도는 몬스터 추가 시 스탯 참고 용입니다.
    // 레벨 1 ~ 5 사이, 스탯값은 레벨*7 정도 
    public static List<MonsterData> monsterList = new List<MonsterData>()
    {
        // 초급 몬스터: 레벨 1 ~ 3
        new MonsterData(1, "나약한 개발자", 5, 2),
        new MonsterData(1, "연습용 허수아비", 5, 2),
        new MonsterData(2, "코드덩어리", 15, 4),
        new MonsterData(2, "공허충", 10, 7),
        new MonsterData(3, "도망친 노예", 15, 5),
        new MonsterData(3, "미니언", 15, 5),

        // 중급 몬스터: 레벨 4 ~ 5
        new MonsterData(4, "딸기괴인", 20, 9),
        new MonsterData(4, "노상강도", 20, 8),
        new MonsterData(5, "대포미니언", 25, 10),
        new MonsterData(5, "꼬인 코드덩어리", 30, 5)

    };
}

// 몬스터 - 인스턴스화 될 클래스
public class Monster
{
    public MonsterData data;
    public int hp;
    public bool isDead;
    public int exp;
    public int gold;

    Random random = new Random();

    // 생성자
    public Monster(MonsterData data)
    {
        this.data = new MonsterData(data);
        this.hp = data.hp;
        this.isDead = false;    // 살아있는 상태로 시작
        this.exp = 1 * data.level;
        this.gold = 100 * data.level;
    }


    // 읽기전용 속성
    public int level => data.level;
    public string name => data.name;
    public int atk => data.atk;


    // 회피 - bool 반환
    public bool Dodge()
    {
        int num = random.Next(1, 101);

        if (num <= 10) { return true; } // 10%확률로 회피    
        else { return false; }
    }
}
