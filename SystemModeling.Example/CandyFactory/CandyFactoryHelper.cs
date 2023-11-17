﻿using System.Collections.Generic;
using SystemModeling.Collections;
using SystemModeling.Example.CandyFactory.Candies;
using SystemModeling.Network;
using SystemModeling.Network.Processors;
using SystemModeling.Network.Selectors;
using SystemModeling.Network.TimeProviders;

namespace SystemModeling.Example.CandyFactory;

internal static class CandyFactoryHelper
{
    public static NetworkModel<ICandy> CreateModel()
    {
        DisposeNode<ICandy> finishedCandy = new()
        {
            DebugName = "Produced Candies"
        };

        DisposeNode<ICandy> defectiveCandy = new()
        {
            DebugName = "Defective Candies"
        };

        QualityControlTimeProvider qualityControlTimeProvider = new();
        ThrowingQueue<ICandy> qualityControlQueue = new();
        PooledNodeProcessor<ICandy> qualityControlProcessor = new(InfiniteProcessors(qualityControlTimeProvider));
        ProcessNode<ICandy> qualityControl = new(qualityControlProcessor, qualityControlQueue)
        {
            DebugName = "Quality Control",
            NextNodeSelector = new QualityControlNodeSelector(finishedCandy, defectiveCandy)
        };

        UniformTimeProvider<ICandy> chocolateConveyorTimeProvider = new(0.5f, 0.6f);
        ThrowingQueue<ICandy> chocolateConveyorQueue = new();
        ChocolateConveyorProcessor chocolateConveyorProcessor = new(InfiniteProcessors(chocolateConveyorTimeProvider));
        ProcessNode<ICandy> chocolateConveyor = new(chocolateConveyorProcessor, chocolateConveyorQueue)
        {
            DebugName = "Chocolate Conveyor",
            NextNodeSelector = new ConstantNodeSelector<ICandy>(qualityControl)
        };

        UniformTimeProvider<ICandy> caramelConveyorTimeProvider = new(0.7f, 0.8f);
        ThrowingQueue<ICandy> caramelConveyorQueue = new();
        CaramelConveyorProcessor caramelConveyorProcessor = new(InfiniteProcessors(caramelConveyorTimeProvider));
        ProcessNode<ICandy> caramelConveyor = new(caramelConveyorProcessor, caramelConveyorQueue)
        {
            DebugName = "Caramel Conveyor",
            NextNodeSelector = new ConstantNodeSelector<ICandy>(qualityControl)
        };

        UniformTimeProvider<ICandy> gummyConveyorTimeProvider = new(0.6f, 0.7f);
        ThrowingQueue<ICandy> gummyConveyorQueue = new();
        GummyConveyorProcessor gummyConveyorProcessor = new(InfiniteProcessors(gummyConveyorTimeProvider));
        ProcessNode<ICandy> gummyConveyor = new(gummyConveyorProcessor, gummyConveyorQueue)
        {
            DebugName = "Gummy Conveyor",
            NextNodeSelector = new ConstantNodeSelector<ICandy>(qualityControl)
        };

        ConstantTimeProvider<ICandy> incomingConveyorTimeProvider = new(1);
        ThrowingQueue<ICandy> incomingConveyorQueue = new();
        PooledNodeProcessor<ICandy> incomingConveyorProcessor = new(InfiniteProcessors(incomingConveyorTimeProvider));
        ProcessNode<ICandy> incomingConveyor = new(incomingConveyorProcessor, incomingConveyorQueue)
        {
            DebugName = "Incoming Conveyor",
            NextNodeSelector = new CreateCandyNodeSelector(chocolateConveyor, caramelConveyor, gummyConveyor)
        };

        CandyFactory factory = new();
        UniformTimeProvider<ICandy> createCandyTimeProvider = new(0.1f, 0.2f);
        ConstantNodeSelector<ICandy> createCandyNodeSelector = new(incomingConveyor);
        CreateNode<ICandy> createCandy = new(factory, createCandyTimeProvider, createCandyNodeSelector)
        {
            DebugName = "Incoming Candy"
        };

        CandyFactoryStatRecorder statRecorder = new();
        finishedCandy.OnEnter += (x, _) => statRecorder.RecordFinishedCandy(x);
        defectiveCandy.OnEnter += (x, _) => statRecorder.RecordDefectiveCandy(x);

        NetworkModel<ICandy> model = new(createCandy, incomingConveyor, chocolateConveyor, caramelConveyor, gummyConveyor, qualityControl, defectiveCandy, finishedCandy);

        model.DebugPrintExtra += statRecorder.DebugWriteStats;
        
        return model;
    }

    private static IEnumerable<INetworkNodeProcessor<T>> InfiniteProcessors<T>(IProcessingTimeProvider<T> timeProvider)
    {
        while (true)
            yield return new SingleNodeProcessor<T>(timeProvider);
    }
}
