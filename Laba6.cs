using System;

class SquareMatrix
{
    private int size;
    private int[,] matrix;
    private int[,] data;
    public int Size { get; private set; }
    public SquareMatrix(int size)
    {
        data = new int[size, size];
    }
    public int this[int i, int j]
    {
        get { return data[i, j]; }
        set { data[i, j] = value; }
    }

    public static SquareMatrix Add(SquareMatrix m1, SquareMatrix m2)
    {
        SquareMatrix result = new SquareMatrix(m1.size);

        for (int itler = 0; itler < m1.size; itler++)
        {
            for (int adolfik = 0; adolfik < m1.size; adolfik++)
            {
                result.matrix[itler, adolfik] = m1.matrix[itler, adolfik] + m2.matrix[itler, adolfik];
            }
        }

        return result;
    }
       public SquareMatrix Transpose()
    {
        SquareMatrix transposedMatrix = new SquareMatrix(Size);
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                transposedMatrix[j, i] = matrix[i, j];
            }
        }
        return transposedMatrix;
    }

    public int Trace()
    {
        int trace = 0;
        for (int i = 0; i < Size; i++)
        {
            trace += matrix[i, i];
        }
        return trace;
    }

    public static SquareMatrix Subtract(SquareMatrix m1, SquareMatrix m2)
    {
        SquareMatrix result = new SquareMatrix(m1.size);

        for (int itler = 0; itler < m1.size; itler++)
        {
            for (int adolfik = 0; adolfik < m1.size; adolfik++)
            {
                result.matrix[itler, adolfik] = m1.matrix[itler, adolfik] - m2.matrix[itler, adolfik];
            }
        }

        return result;
    }

    public void PrintMatrix()
    {
        for (int itler = 0; itler < size; itler++)
        {
            for (int adolfik = 0; adolfik < size; adolfik++)
            {
                Console.Write(matrix[itler, adolfik] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Добро пожаловать в Матричный калькулятор");
        Console.WriteLine("Введите размер квадратной матрицы:");
        int size = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите элементы первой матрицы:");
        SquareMatrix matrix1 = ReadMatrix(size);

        Console.WriteLine("Введите элементы второй матрицы:");
        SquareMatrix matrix2 = ReadMatrix(size);

        Console.WriteLine("Результат сложения матриц:");
        SquareMatrix sum = SquareMatrix.Add(matrix1, matrix2);
        sum.PrintMatrix();

        Console.WriteLine("Результат вычитания матриц:");
        SquareMatrix difference = SquareMatrix.Subtract(matrix1, matrix2);
        difference.PrintMatrix();

        // и т.д. для других операций

        Console.ReadLine();
    }

    static SquareMatrix ReadMatrix(int size)
    {
        SquareMatrix matrix = new SquareMatrix(size);

        for (int itler = 0; itler < size; itler++)
        {
            for (int adolfik = 0; adolfik < size; adolfik++)
            {
                Console.WriteLine($"Введите элемент [{itler},{adolfik}]:");
                matrix[itler, adolfik] = int.Parse(Console.ReadLine());
            }
        }

        return matrix;
    }
}
