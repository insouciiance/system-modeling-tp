using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SystemModeling.Example.CandyFactory.Candies;

namespace SystemModeling.Example.CandyFactory;

internal class CandyFactoryStatRecorder
{
    public float Income { get; private set; }

    public int FinishedCandy { get; private set; }

    public Dictionary<Type, int> DefectiveCandy { get; } = new()
    {
        { typeof(ChocolateCandy), 0 },
        { typeof(CaramelCandy), 0 },
        { typeof(GummyCandy), 0 }
    };

    public int DefectiveCandyCount => DefectiveCandy.Sum(x => x.Value);

    public void RecordFinishedCandy(ICandy candy)
    {
        Income += candy.GetPrice();
        FinishedCandy++;
    }

    public void RecordDefectiveCandy(ICandy candy)
    {
        DefectiveCandy[candy.GetType()]++;
    }

    public void DebugWriteStats()
    {
        Trace.Listeners.Add(new ConsoleTraceListener());
        Debug.WriteLine($"Total income: {Income}");
        Debug.WriteLine($"Finished candy: {FinishedCandy}");
        Debug.WriteLine($"Defective candy: {DefectiveCandyCount}");
        Debug.WriteLine($"Percentage of defective candy: {100f * DefectiveCandyCount / (FinishedCandy + DefectiveCandyCount)}%");
        Debug.WriteLine("Percentage of defective candy by type:");
        Debug.WriteLine($"Chocolate candy: {100f * DefectiveCandy[typeof(ChocolateCandy)] / DefectiveCandyCount}%");
        Debug.WriteLine($"Caramel candy: {100f * DefectiveCandy[typeof(CaramelCandy)] / DefectiveCandyCount}%");
        Debug.WriteLine($"Gummy candy: {100f * DefectiveCandy[typeof(GummyCandy)] / DefectiveCandyCount}%");
    }
}
