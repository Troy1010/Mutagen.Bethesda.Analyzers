﻿using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Analyzers.SDK.Topics;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Skyrim.Internals;
using Noggog;

namespace Mutagen.Bethesda.Analyzers.Skyrim;

// Just a proof of concept
// Will likely want a more generic or autogenerated solution for one like list counter
public class ListCounterAnalyzer : IIsolatedRecordFrameAnalyzer<IArmorGetter>
{
    public static readonly TopicDefinition<uint, int> ListCounterWrong = MutagenTopicBuilder.FromDiscussion(
            ushort.MaxValue,
            "List count mismatch",
            Severity.Warning)
        .WithFormatting<uint, int>("Counter lists {0} items, but actually found {1}");

    public RecordFrameAnalyzerResult? AnalyzeRecord(IsolatedRecordFrameAnalyzerParams<IArmorGetter> param)
    {
        if (!param.Frame.TryFindSubrecord(RecordTypes.KSIZ, out var ksizFrame)) return null;
        if (!param.Frame.TryFindSubrecord(RecordTypes.KWDA, out var kwdaFrame)) return null;
        var count = ksizFrame.AsUInt32();
        var actualCount = kwdaFrame.ContentLength / 4;
        if (count == actualCount) return null;
        return new RecordFrameAnalyzerResult(
            RecordFrameTopic.Create(
                ListCounterWrong.Format(count, actualCount)));
    }

    public IEnumerable<TopicDefinition> Topics => ListCounterWrong.AsEnumerable();
}
