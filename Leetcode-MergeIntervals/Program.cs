namespace Leetcode_MergeIntervals;

internal abstract class Program
{
    private static void Main()
    {
        const int maxAttempts = 5;
        var attemptCount = 0;
        string? input;
        string? validationMessage;

        do
        {
            Console.WriteLine(
                "Введите интервалы состоящие из парных чисел (например [[1,3],[2,6],[8,10],[15,18]] быдет записан как: 1,3,2,6,8,10,15,18) " +
                "и мы объединим все перекрывающиеся интервалы и вернем массив неперекрывающихся интервалов, которые охватывают все интервалы во входных данных.");
            Console.WriteLine("Правила:");
            Console.WriteLine("1) Введенные значения должны быть целыми числами");
            Console.WriteLine("2) Значения должны быть в диапазоне от 0 до 10^4.");
            Console.WriteLine("3) Первое значение в паре должно быть меньше или равно второму");
            Console.WriteLine("4) Числа долдны быть парами, то есть должно быть четное количество чисел");
            Console.WriteLine($"У вас осталось {maxAttempts - attemptCount} попыток.");
            input = Console.ReadLine();

            validationMessage = ValidateInput(input);

            if (validationMessage == null) continue;
            Console.WriteLine(validationMessage);
            attemptCount++;

            if (attemptCount >= maxAttempts)
            {
                Console.WriteLine($"Превышено максимальное количество попыток ({maxAttempts}). Программа завершена.");
                return;
            }

            Console.WriteLine($"Пожалуйста, повторите ввод. Попытка {attemptCount}/{maxAttempts}.");

        } while (validationMessage != null);

        var inputIntervals = input!.Split(',');

        var intervals = new int[inputIntervals.Length / 2][];
        for (int i = 0, j = 0; i < inputIntervals.Length; i += 2, j++)
        {
            intervals[j] = new[] { int.Parse(inputIntervals[i]), int.Parse(inputIntervals[i + 1]) };
        }

        var result = MergeIntervals(intervals);
            Console.WriteLine("Вывод: " + "[" + string.Join("], [", result.Select(interval => string.Join(", ", interval))) + "]");
    }

    private static IEnumerable<int[]> MergeIntervals(int[][]? intervals)
    {
        if (intervals == null || intervals.Length == 0)
            return Array.Empty<int[]>();

        intervals = intervals.OrderBy(interval => interval[0]).ToArray();

        var mergedIntervals = new List<int[]>();
        var currentInterval = intervals[0];

        foreach (var interval in intervals)
        {
            if (interval[0] <= currentInterval[1])
            {
                currentInterval[1] = Math.Max(currentInterval[1], interval[1]);
            }
            else
            {
                mergedIntervals.Add(currentInterval);
                currentInterval = interval;
            }
        }

        mergedIntervals.Add(currentInterval);
        return mergedIntervals.ToArray();
    }

    private static string? ValidateInput(string? input)
    {
        if (input == null) return null;
        var inputIntervals = input.Split(',');

        if (string.IsNullOrWhiteSpace(input)) return "Вы ничего не ввели.";

        if (inputIntervals.Any(value => !int.TryParse(value, out _))) return "Вы ввели не только цифры.";
        
        if (inputIntervals.Length % 2 != 0) return "Вы ввели нечетное количество цифр.";
        for (var i = 0; i < inputIntervals.Length; i += 2)
        {
            if (!int.TryParse(inputIntervals[i], out var start) || !int.TryParse(inputIntervals[i + 1], out var end))
            {
                return "Введенные значения должны быть целыми числами.";
            }

            if (start < 0 || end > (int)Math.Pow(10, 4))
            {
                return "Значения должны быть в диапазоне от 0 до 10^4.";
            }

            if (start > end)
            {
                return "Значение 'start' должно быть меньше или равно 'end'.";
            }
        }

        return null;
    }
}