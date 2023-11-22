using System.Diagnostics;
using SystemModeling.Example.CandyFactory;
using SystemModeling.Example.CandyFactory.Candies;

namespace SystemModeling.Benchmark;

internal class Program
{
    public static void Main(string[] args)
    {
        Trace.Listeners.Clear();

        DelayedStatisticsPolicy<ICandy> policy = new(300);

        const int runsCount = 20;

        float averageFailurePercentage = 0;

        for (int i = 0; i < runsCount; i++)
        {
            Console.WriteLine($"Run #{i}");
            var model = CandyFactoryHelper.CreateModel(policy, out var recorder);

            model.Simulate(1300);
            Console.WriteLine();

            averageFailurePercentage += recorder.GetDefectiveCandyPercentage();
        }

        averageFailurePercentage /= runsCount;

        Console.WriteLine($"Total average failure percentage: {averageFailurePercentage}");
    }
}
