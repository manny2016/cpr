


namespace Org.Joey.Common
{
    using System;
    [Flags]
    public enum LeadStates
    {
        /// <summary>
        /// synchronized to TFS
        /// </summary>
        IngestToVSO = 1,
        /// <summary>
        /// synchronized to MSX and not confirm 
        /// </summary>
        IngestToMSX = 2,
        /// <summary>
        /// Update inTFS value in Azure Storage
        /// </summary>
        UpdatedInAzureStorage = 4,
        /// <summary>
        /// 
        /// </summary>
        FailedInMSX = 8,
        /// <summary>
        /// 
        /// </summary>
        SuccessInMSX = 16,
    }
}
