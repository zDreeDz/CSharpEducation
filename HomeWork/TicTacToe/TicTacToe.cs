char[] board = new char[9];

while (true)
{
    ResetBoard(board);
    char currentPlayer = 'X';

    while (true)
    {
        Console.Clear();
        Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
        Console.WriteLine();
        DrawBoard(board);
        Console.WriteLine();
        Console.WriteLine($"Ход игрока {currentPlayer}. Введите число от 1 до 9.");

        int moveIndex = ReadMove(board);
        board[moveIndex] = currentPlayer;

        if (HasWinner(board, currentPlayer))
        {
            Console.Clear();
            Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
            Console.WriteLine();
            DrawBoard(board);
            Console.WriteLine();
            Console.WriteLine($"Игрок {currentPlayer} победил!");
            break;
        }

        if (IsBoardFull(board))
        {
            Console.Clear();
            Console.WriteLine("КРЕСТИКИ-НОЛИКИ");
            Console.WriteLine();
            DrawBoard(board);
            Console.WriteLine();
            Console.WriteLine("Ничья!");
            break;
        }

        currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
    }

    Console.WriteLine();
    Console.Write("Сыграть еще раз? (y/n): ");
    string? answer = Console.ReadLine()?.Trim().ToLowerInvariant();
    if (answer is not ("y" or "д" or "да"))
    {
        break;
    }
}

static void ResetBoard(char[] board)
{
    for (int i = 0; i < board.Length; i++)
    {
        board[i] = ' ';
    }
}

static void DrawBoard(char[] board)
{
    for (int row = 0; row < 3; row++)
    {
        int start = row * 3;
        string c1 = CellText(board, start);
        string c2 = CellText(board, start + 1);
        string c3 = CellText(board, start + 2);

        Console.WriteLine($" {c1} | {c2} | {c3} ");
        if (row < 2)
        {
            Console.WriteLine("---+---+---");
        }
    }
}

static string CellText(char[] board, int index)
{
    return board[index] == ' ' ? (index + 1).ToString() : board[index].ToString();
}

static int ReadMove(char[] board)
{
    while (true)
    {
        Console.Write("Ваш ход: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int cellNumber))
        {
            Console.WriteLine("Ошибка: введите число от 1 до 9.");
            continue;
        }

        if (cellNumber < 1 || cellNumber > 9)
        {
            Console.WriteLine("Ошибка: ячейка должна быть в диапазоне 1..9.");
            continue;
        }

        int index = cellNumber - 1;
        if (board[index] != ' ')
        {
            Console.WriteLine("Ошибка: эта ячейка уже занята.");
            continue;
        }

        return index;
    }
}

static bool IsBoardFull(char[] board)
{
    for (int i = 0; i < board.Length; i++)
    {
        if (board[i] == ' ')
        {
            return false;
        }
    }

    return true;
}

static bool HasWinner(char[] board, char player)
{
    int[][] lines =
    [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6]
    ];

    foreach (int[] line in lines)
    {
        if (board[line[0]] == player &&
            board[line[1]] == player &&
            board[line[2]] == player)
        {
            return true;
        }
    }

    return false;
}
