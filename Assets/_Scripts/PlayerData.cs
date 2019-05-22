public class PlayerData
{
    public int UserId { get; private set; }
    public string Login { get; private set; }
    public bool[] ObjectsShotDown { get; private set; }

    public PlayerData(int userId, string login, bool[] objectsShotDown)
    {
        UserId = userId;
        Login = login;
        ObjectsShotDown = objectsShotDown;
    }
}
