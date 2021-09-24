﻿using Mutagen.Bethesda.Analyzers.SDK.Analyzers;
using Mutagen.Bethesda.Analyzers.SDK.Errors;
using Mutagen.Bethesda.Analyzers.SDK.Results;
using Mutagen.Bethesda.Skyrim;

namespace Mutagen.Bethesda.SkyrimAnalyzer
{
    public partial class MissingAssetsAnalyzer : IMajorRecordAnalyzer<IStaticGetter>
    {
        public static readonly ErrorDefinition MissingStaticModel = new(
            "SOMEID",
            "Missing Static Model file",
            MissingModelFileMessageFormat,
            Severity.Error);

        public MajorRecordAnalyzerResult AnalyzeRecord(IStaticGetter staticGetter)
        {
            var res = new MajorRecordAnalyzerResult();
            CheckForMissingModelAsset(staticGetter, res, MissingStaticModel);
            return res;
        }
    }
}