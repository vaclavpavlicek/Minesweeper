using System;

namespace session_20170124
{
    public class Minesweeper
    {
        private bool[,] field;
        private bool[,] viewField;
        private static readonly char HIDDEN_CELL = '-';
        private static readonly char VISIBLE_CELL_WITHOUT_BOMBS_IN_NEIGHBORHOOD = ' ';

        public Minesweeper(bool[,] field)
        {
            this.field = field;
            viewField = new bool[field.GetLength(0), field.GetLength(1)];
        }

        public bool IsBomb(int x, int y)
        {
            if (IsntInRange(x, 0, field.GetLength(0)) || IsntInRange(y, 0, field.GetLength(1)))
                return false;
            return field[x, y];
        }

        private static bool IsntInRange(int index, int min, int max)
        {
            return index < min || index >= max;
        }

        public int GetCellScore(int x, int y)
        {
            var numberOfBombs = 0;
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (!(i == x && j == y) && IsBomb(i, j))
                    {
                        numberOfBombs++;
                    }
                }
            }

            return numberOfBombs;
        }

        public char ShowCell(int x, int y)
        {
            int CellScore = GetCellScore(x, y);
            return viewField[x, y] ? (CellScore > 0 ? Convert.ToChar(CellScore.ToString()) : VISIBLE_CELL_WITHOUT_BOMBS_IN_NEIGHBORHOOD) : HIDDEN_CELL;
        }

        public bool CellHasAtLeastOneBombInNeighborhood(int x, int y)
        {
            return GetCellScore(x, y) > 0;
        }

        public bool IsPositionValid(int x, int y)
        {
            return x >= 0 & x < viewField.GetLength(0) && y >= 0 & y < viewField.GetLength(1);
        }

        public void CellChoosen(int x, int y)
        {
            if (viewField[x, y])
            {
                return;
            }

            viewField[x, y] = true;
            if (CellHasAtLeastOneBombInNeighborhood(x, y))
            {
                return;
            }

            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (IsPositionValid(i, j))
                    {
                        CellChoosen(i, j);
                    }
                }
            }
        }

        public String GetActualViewOfField()
        {
            String result = " ";
            for (var i = 0; i < viewField.GetLength(1) - 1; i++)
            {
                result += i.ToString() + " ";
            }
            result += (viewField.GetLength(1) - 1).ToString() + "\n";
            for (var i = 0; i < viewField.GetLength(0); i++)
            {
                result += i.ToString();
                for (var j = 0; j < viewField.GetLength(1) - 1; j++)
                {
                    result += ShowCell(i, j) + " ";
                }
                result += ShowCell(i, viewField.GetLength(1) - 1) + "\n";
            }

            return result;
        }

        public String NextRound(int x, int y)
        {
            CellChoosen(x, y);
            if (IsBomb(x, y))
            {
                return "Game over!";
            }
            if (PlayerWin())
            {
                return "You win!";
            }

            return GetActualViewOfField();
        }

        public bool PlayerWin()
        {
            for (var i = 0; i < viewField.GetLength(0); i++)
            {
                for (var j = 0; j < viewField.GetLength(1); j++)
                {
                    if (!viewField[i, j] && !field[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

