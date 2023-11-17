using System.Diagnostics;
using SystemModeling.Example.CandyFactory;

//Trace.Listeners.Add(new ConsoleTraceListener());

var model = CandyFactoryHelper.CreateModel();
model.Simulate(1000);
