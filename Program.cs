﻿int[] inputBuffer = { };

void showHelp()
{
    string action = inputBuffer.Length > 0 ? "продолжить" : "начать";
    Console.WriteLine($"Нажмите 1 + Enter, для выполнения команды 1: {action} ввод элементов массива");
    if (inputBuffer.Length > 0)
    {
        Console.WriteLine("Нажмите 2 + Enter, для выполнения команды 2: завершить ввод элементов массива и вывести результат");
        Console.WriteLine("Нажмите 3 + Enter, для выполнения команды 3: очистить массив");
    }
    Console.WriteLine("Нажмите 4 + Enter, для выполнения команды 4: задать параметры генерации массива случайных чисел и вывести результат");
    Console.WriteLine("Нажмите 5 + Enter, для выхода из программы");
}

int[] filterEvens(int[] array)
{
    int[] result = { };
    int[] tmpArray = new int[10];
    int tal = 0;

    for (int i = 0; i < array.Length; i++)
    {
        if (array[i] % 2 != 0)
        {
            continue;
        }
        tmpArray[tal++] = array[i];
        if (tal >= tmpArray.Length)
        {
            int oldSize = result.Length;
            Array.Resize(ref result, oldSize + tal);
            Array.ConstrainedCopy(tmpArray, 0, result, oldSize, tal);
            tal = 0;
        }
    }

    if (tal > 0)
    {
        int oldSize = result.Length;
        Array.Resize(ref result, oldSize + tal);
        Array.ConstrainedCopy(tmpArray, 0, result, oldSize, tal);
    }

    return result;
}

string arrayToString(int[] array)
{
    return "{" + String.Join(", ", array) + "}";
}

void startInput()
{
    Console.WriteLine($"Введите целое число что бы добавить элемент в массив {arrayToString(inputBuffer)}");
    Console.WriteLine("Что бы выйти из режима ввода массива нажмите Enter");
    while (true)
    {
        string? nextInput = Console.ReadLine();
        if (nextInput == "")
        {
            break;
        }
        int nextNumber;
        try
        {
            nextNumber = int.Parse(nextInput!.Replace(" ", ""));
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine($"Извините, не могу распознать число {nextInput}");
            continue;
        }
        catch (FormatException)
        {
            Console.WriteLine($"Извините, не могу распознать число {nextInput} ошибка формата");
            continue;
        }
        catch (OverflowException)
        {
            Console.WriteLine($"Извините, число {nextInput} выходит за рамки интервала {int.MinValue} .. {int.MaxValue}");
            continue;
        }
        Array.Resize(ref inputBuffer, inputBuffer.Length + 1);
        inputBuffer[inputBuffer.Length - 1] = nextNumber;
    }
}

void finishInput()
{
    Console.WriteLine("Входные данные");
    Console.WriteLine(arrayToString(inputBuffer));
    int[] resultArray = filterEvens(inputBuffer);
    Console.WriteLine("Выходные данные");
    Console.WriteLine(arrayToString(resultArray));
}

