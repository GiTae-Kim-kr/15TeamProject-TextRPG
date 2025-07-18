using _15TeamProject;

class Program
{
    
    static void Main(String[] args)
    {
        // 오프닝 실행
        Opening opening = new Opening();
        opening.Run();
        
        // 게임 시작
        StartScene.Instance.GameStartScene();

    }
}