using System;
using System.Text;

// Класс исключения для неправильного размера матрицы
class InvalidMatrixSizeException : Exception
{
    public InvalidMatrixSizeException(string message) : base(message)
    {
    }
}

// Класс исключения для неправильной операции
class InvalidOperationException : Exception
{
    public InvalidOperationException(string message) : base(message)
    {
    }
}

// Класс квадратная матрица
class Matrix
{
    private int[,] data; // Двумерный массив для хранения элементов матрицы

    public int Size { get; } // Размер матрицы

    // Конструктор для создания матрицы заданного размера
    public Matrix(int size)
    {
        if (size <= 0)
        {
            throw new InvalidMatrixSizeException("Размер матрицы должен быть больше нуля");
        }

        Size = size;
        data = new int[Size, Size];
    }

    // Конструктор для создания матрицы на основе двумерного массива
    public Matrix(int[,] matrixData)
    {
        if (matrixData == null || matrixData.GetLength(0) != matrixData.GetLength(1))
        {
            throw new InvalidMatrixSizeException("Неправильный размер массива");
        }

        Size = matrixData.GetLength(0);
        data = new int[Size, Size];

        Array.Copy(matrixData, data, Size * Size);
    }

    // Метод для генерации случайной матрицы
    public static Matrix GenerateRandomMatrix(int size, int min = 0, int max = 100)
    {
        Random random = new Random();
        Matrix randomMatrix = new Matrix(size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                randomMatrix.data[i, j] = random.Next(min, max);
            }
        }

        return randomMatrix;
    }

    // Перегрузка оператора сложения (+) для матриц
    public static Matrix operator +(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        Matrix resultMatrix = new Matrix(matrix1.Size);

        for (int i = 0; i < matrix1.Size; i++)
        {
            for (int j = 0; j < matrix1.Size; j++)
            {
                resultMatrix.data[i, j] = matrix1.data[i, j] + matrix2.data[i, j];
            }
        }

        return resultMatrix;
    }

    // Перегрузка оператора умножения (*) для матриц
    public static Matrix operator *(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        Matrix resultMatrix = new Matrix(matrix1.Size);

        for (int i = 0; i < matrix1.Size; i++)
        {
            for (int j = 0; j < matrix1.Size; j++)
            {
                for (int k = 0; k < matrix1.Size; k++)
                {
                    resultMatrix.data[i, j] += matrix1.data[i, k] * matrix2.data[k, j];
                }
            }
        }

        return resultMatrix;
    }

    // Перегрузка оператора больше (>)
    public static bool operator >(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        for (int i = 0; i < matrix1.Size; i++)
        {
            for (int j = 0; j < matrix1.Size; j++)
            {
                if (matrix1.data[i, j] <= matrix2.data[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Перегрузка оператора меньше (<)
    public static bool operator <(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        for (int i = 0; i < matrix1.Size; i++)
        {
            for (int j = 0; j < matrix1.Size; j++)
            {
                if (matrix1.data[i, j] >= matrix2.data[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Перегрузка оператора больше или равно (>=)
    public static bool operator >=(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        return matrix1 > matrix2 || matrix1 == matrix2;
    }

    // Перегрузка оператора меньше или равно (<=)
    public static bool operator <=(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        return matrix1 < matrix2 || matrix1 == matrix2;
    }

    // Перегрузка оператора равно (==)
    public static bool operator ==(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.Size != matrix2.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        for (int i = 0; i < matrix1.Size; i++)
        {
            for (int j = 0; j < matrix1.Size; j++)
            {
                if (matrix1.data[i, j] != matrix2.data[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Перегрузка оператора неравно (!=)
    public static bool operator !=(Matrix matrix1, Matrix matrix2)
    {
        return !(matrix1 == matrix2);
    }

    // Перегрузка оператора приведения типа
    public static explicit operator int(Matrix matrix)
    {
        if (matrix.Size != 1)
        {
            throw new InvalidOperationException("Ошибка приведения типа");
        }

        return matrix.data[0, 0];
    }

    // Перегрузка оператора true
    public static bool operator true(Matrix matrix)
    {
        for (int i = 0; i < matrix.Size; i++)
        {
            for (int j = 0; j < matrix.Size; j++)
            {
                if (matrix.data[i, j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    // Перегрузка оператора false
    public static bool operator false(Matrix matrix)
    {
        return !(matrix);
    }

    // Метод для нахождения детерминанта матрицы
    public double GetDeterminant()
    {
        if (Size == 1)
        {
            return data[0, 0];
        }

        if (Size == 2)
        {
            return data[0, 0] * data[1, 1] - data[0, 1] * data[1, 0];
        }

        double determinant = 0;

        for (int i = 0; i < Size; i++)
        {
            determinant += Math.Pow(-1, i) * data[0, i] * GetMinor(0, i).GetDeterminant();
        }

        return determinant;
    }

    // Метод для нахождения минора матрицы
    public Matrix GetMinor(int row, int col)
    {
        if (row < 0 || row >= Size || col < 0 || col >= Size)
        {
            throw new IndexOutOfRangeException("Неправильные индексы строки и столбца");
        }

        int[,] minorData = new int[Size - 1, Size - 1];
        int minorRow = 0, minorCol = 0;

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (i != row && j != col)
                {
                    minorData[minorRow, minorCol] = data[i, j];
                    minorCol++;

                    if (minorCol == Size - 1)
                    {
                        minorCol = 0;
                        minorRow++;
                    }
                }
            }
        }

        return new Matrix(minorData);
    }

    // Метод для нахождения обратной матрицы
    public Matrix GetInverseMatrix()
    {
        double determinant = GetDeterminant();

        if (determinant == 0)
        {
            throw new InvalidOperationException("Обратная матрица не существует");
        }

        Matrix transposeMatrix = new Matrix(Size);

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                transposeMatrix.data[j, i] = Math.Pow(-1, i + j) * GetMinor(i, j).GetDeterminant();
            }
        }

        Matrix inverseMatrix = (1 / determinant) * (transposeMatrix);

        return inverseMatrix;
    }

    // Перегрузка метода ToString() для вывода матрицы в виде строки
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                stringBuilder.Append(data[i, j]);
                stringBuilder.Append(" ");
            }

            stringBuilder.Append(Environment.NewLine);
        }

        return stringBuilder.ToString();
    }

    // Перегрузка метода CompareTo() для сравнения двух матриц
    public int CompareTo(Matrix other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Size != other.Size)
        {
            throw new InvalidOperationException("Неправильный размер матрицы");
        }

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                if (data[i, j] < other.data[i, j])
                {
                    return -1;
                }
                else if (data[i, j] > other.data[i, j])
                {
                    return 1;
                }
            }
        }

        return 0;
    }

    // Перегрузка метода Equals() для сравнения двух матриц
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Matrix other = (Matrix)obj;

        return this == other;
    }

    // Перегрузка метода GetHashCode()
    public override int GetHashCode()
    {
        return Size.GetHashCode() ^ data.GetHashCode();
    }

    // Реализация паттерна "Прототип"
    public Matrix Clone()
    {
        Matrix cloneMatrix = new Matrix(Size);

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                cloneMatrix.data[i, j] = data[i, j];
            }
        }

        return cloneMatrix;
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Создание и вывод случайной матрицы
            Console.WriteLine("Случайная матрица:");
            Matrix randomMatrix = Matrix.GenerateRandomMatrix(3);
            Console.WriteLine(randomMatrix);

            // Проверка перегрузки операторов сложения и умножения
            Console.WriteLine("Результат сложения матриц:");
            Matrix sumMatrix = randomMatrix + randomMatrix;
            Console.WriteLine(sumMatrix);

            Console.WriteLine("Результат умножения матриц:");
            Matrix productMatrix = randomMatrix * randomMatrix;
            Console.WriteLine(productMatrix);

            // Проверка перегрузки операторов сравнения
            Console.WriteLine("Сравнение матриц:");
            Console.WriteLine(randomMatrix > sumMatrix);
            Console.WriteLine(randomMatrix < sumMatrix);
            Console.WriteLine(randomMatrix >= sumMatrix);
            Console.WriteLine(randomMatrix <= sumMatrix);
            Console.WriteLine(randomMatrix == sumMatrix);
            Console.WriteLine(randomMatrix != sumMatrix);

            // Проверка методов приведения типов, true и false
            Console.WriteLine("Приведение типа матрицы к int:");
            int value = (int)randomMatrix;
            Console.WriteLine(value);

            Console.WriteLine("Проверка условий true и false:");
            Console.WriteLine(randomMatrix);
            Console.WriteLine(randomMatrix is true);
            Console.WriteLine(randomMatrix is false);

            // Проверка нахождения детерминанта и обратной матрицы
            Console.WriteLine("Нахождение детерминанта и обратной матрицы:");
            Console.WriteLine(randomMatrix.GetDeterminant());
            Matrix inverseMatrix = randomMatrix.GetInverseMatrix();
            Console.WriteLine(inverseMatrix);

            // Проверка методов ToString(), CompareTo() и Equals()
            Console.WriteLine("Проверка методов ToString(), CompareTo() и Equals():");
            Console.WriteLine(randomMatrix.ToString());
            Console.WriteLine(randomMatrix.CompareTo(sumMatrix));
            Console.WriteLine(randomMatrix.Equals(sumMatrix));

            // Проверка паттерна "Прототип"
            Console.WriteLine("Клонирование матрицы:");
            Matrix cloneMatrix = randomMatrix.Clone();
            Console.WriteLine(cloneMatrix);
        }
        catch (InvalidMatrixSizeException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        Console.ReadLine();
    }
}
