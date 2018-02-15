using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void doSearch(char[][] charMatrix, int x, int y, int dx, int dy, char typeS)
        {
            //Процедура поиска повторяющичся последовательностей по задаваемому направлению
            //dx=1,dy=0 - по вертикали; dx=0,dy=1 - по горизонтали; dx=1,dy=1 - по диагонали; dx=1,dy=1 - по обратной диагонали 
            int x_i, y_i, len;
            int M = charMatrix.Length;      
            int N = charMatrix[0].Length;
            if (x >= M || y >= N || x < 0 || y < 0) return;
            do
            {
                x_i = x + dx;
                y_i = y + dy;
                len = 0;
                while ((x_i >= 0) && (y_i >= 0) && (x_i < M) && (y_i < N) && (charMatrix[x][y] == charMatrix[x_i][y_i]))
                {
                    x_i += dx;
                    y_i += dy;
                    len++;
                }
                if (len > 0)
                    Console.WriteLine("{0} [{1} {2}] {3} {4}", typeS, x + 1, y + 1, charMatrix[x][y], len + 1);
                x = x_i;
                y = y_i;
            } while ((x + dx >= 0) && (y + dy >= 0) && (x + dx < M) && (y + dy < N));
        }
        static void Main(string[] args)
        {
            byte M, N;
            char[][] matrix;
            Console.WriteLine("Введите M, N (через пробел\\запятую\\двоеточие)");
            string rLine = Console.ReadLine();
            string[] spLine = rLine.Split(new[] { ' ', ',', ':' }, StringSplitOptions.RemoveEmptyEntries);
            try
	    {
                M = Byte.Parse(spLine[0]);
                N = Byte.Parse(spLine[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Неправильно введены M или N");
                Console.WriteLine(e.Message);
                return;
            }
            matrix = new char[M][];
            for (byte i = 0; i < M; i++)
            {
                Console.WriteLine("Введите {0} строку из {1} символов", i + 1, N);
                rLine = Console.ReadLine();
                string rtrimLine = System.Text.RegularExpressions.Regex.Replace(rLine, @"\s+", "");
                if (rtrimLine.Length != N)
                {
		    Console.WriteLine("Неверное количество символов. Введите {0} символов", N);
		    --i;
		    continue;
                }
                matrix[i] = rtrimLine.ToCharArray();
            }
            for (byte x = 0; x < matrix.Length; x++)
            {
                for (byte y = 0; y < matrix[x].Length; y++)
				{
					Console.Write(matrix[x][y]);
                    Console.Write(' ');
                }
                Console.WriteLine("");
            }
            //В циклах матрица последовательно обходится с левой стороны, 
            //сверху и справа.
            for (byte x = 0; x < matrix.Length; x++)
            {
                //Находятся повторяющиеся последовательности по горизонтали и диагонали
                doSearch(matrix, x, 0, 0, 1, '-');
                doSearch(matrix, x, 0, 1, 1, '\\');
            }
            for (byte y = 0; y < matrix[0].Length; y++)
            {
                //Находятся повторяющиеся последовательности по вертикали и 2ум диагоналям
                doSearch(matrix, 0, y, 1, 0, '|');
                doSearch(matrix, 0, y + 1, 1, 1, '\\');
                doSearch(matrix, 0, y + 1, 1, -1, '/');
            }
            for (byte x = 1; x < matrix.Length-1; x++)
            {
                //Находятся повторяющиеся последовательности обратной диагонали
                doSearch(matrix, x, N - 1, 1, -1, '/');
            }
            Console.WriteLine("Готово");            
        }
    }
}
