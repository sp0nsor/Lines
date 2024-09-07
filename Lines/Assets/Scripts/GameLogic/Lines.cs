public delegate void ShowBox(int i, int j, int ball);
public delegate void PlayCut();
public class Lines
{
    public const int BTN_SIZE = 9;
    public const int IMG_SIZE = 7;
    private const int ADD_BALLS = 3;
    private const int LINE_LENGTH = 4;

    private int[,] _map;
    private bool[,] _marks;
    private ShowBox _showBox;
    private PlayCut _playCut;
    private System.Random _random = new System.Random();
    private (int i, int j) _moveFrom;
    private bool _isBallSelected;

    public Lines(ShowBox showBox, PlayCut playCut)
    {
        _showBox = showBox;
        _playCut = playCut;
        _map = new int[BTN_SIZE, BTN_SIZE];
    }

    public void Start()
    {
        ClearMap();
        AddRandomBalls();
        _isBallSelected = false;
    }

    public void Click(int i, int j)
    {
        if (IsGameOver())
            Start();
        if (_map[i, j] > 0)
            TakeBall(i, j);
        else
            MoveBall(i, j);
    }

    private void TakeBall(int i, int j)
    {
        _moveFrom = (i, j);
        _isBallSelected = true;
    }

    private void MoveBall(int i, int j)
    {
        if (!_isBallSelected) return;
        SetMap(i, j, _map[_moveFrom.i, _moveFrom.j]);
        SetMap(_moveFrom.i, _moveFrom.j, 0);
        _isBallSelected = false;
        if (!CutLines())
        {
            AddRandomBalls();
            CutLines();
        }
    }

    private void ClearMap()
    {
        for (int i = 0; i < BTN_SIZE; i++)
            for (int j = 0; j < BTN_SIZE; j++)
                SetMap(i, j, 0);
    }

    private void SetMap(int i, int j, int ball)
    {
        _map[i, j] = ball;
        _showBox(i, j, ball);
    }

    private void AddRandomBalls()
    {
        for(int i = 0; i < ADD_BALLS; i++)
            AddRandomBall();
    }

    private void AddRandomBall()
    {
        int i, j;
        int loop = BTN_SIZE * BTN_SIZE;
        do
        {
            i = _random.Next(BTN_SIZE);
            j = _random.Next(BTN_SIZE);
            if (--loop <= 0) return;

        } while (_map[i, j] > 0);
        int ball = 1 + _random.Next(IMG_SIZE - 1);
        SetMap(i, j, ball);
    }

    private bool CutLines()
    {
        int balls  = 0;
        _marks = new bool[BTN_SIZE, BTN_SIZE];
        
        for(int i = 0; i < BTN_SIZE; i++)
        {
            for(int j = 0; j < BTN_SIZE; j++)
            {
                balls += FindLine(i, j, 1, 0);
                balls += FindLine(i, j, 0, 1);
                balls += FindLine(i, j, 1, 1);
                balls += FindLine(i, j, -1, 1);
            }
        }
        if (balls > 0)
        {
            _playCut();
            for(int i= 0; i < BTN_SIZE; i++)
                for(int j= 0; j < BTN_SIZE; j++)
                    if(_marks[i, j])
                        SetMap(i, j, 0);

            return true;
        }

        return false;
    }

    private int FindLine(int startX, int startY, int deltaX, int deltaY)
    {
        int count = 0;
        int ball = _map[startX, startY];
        if (ball == 0) return 0;

        for (int x = startX, y = startY; GetMap(x, y) == ball; x += deltaX, y += deltaY)
            count++;

        if (count < LINE_LENGTH) return 0;

        for (int x = startX, y = startY; GetMap(x, y) == ball; x += deltaX, y += deltaY)
            _marks[x, y] = true;

        return count;
    }

    private bool IsGameOver()
    {
        for (int i = 0; i < BTN_SIZE; i++)
            for (int j = 0; j < BTN_SIZE; j++)
                if (_map[i, j] == 0)
                    return false;

        return true;
    }

    private int GetMap(int i, int j)
    {
        if (!OnMap(i, j))
            return 0;

        return _map[i, j];
    }

    private bool OnMap(int x, int y)
    {
        return x >= 0 && x < BTN_SIZE && y >= 0 && y < BTN_SIZE;
    }
}
