// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BuildXL.Cache.ContentStore.Hashing;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.Agent.Blob;
using Microsoft.VisualStudio.Services.Agent.Util;
using BlobIdentifierWithBlocks = Microsoft.VisualStudio.Services.BlobStore.Common.BlobIdentifierWithBlocks;
using VsoHash = Microsoft.VisualStudio.Services.BlobStore.Common.VsoHash;
using Microsoft.VisualStudio.Services.BlobStore.WebApi;
using Microsoft.VisualStudio.Services.Content.Common;
using Microsoft.VisualStudio.Services.Content.Common.Tracing;
using Microsoft.VisualStudio.Services.BlobStore.Common;
using Microsoft.VisualStudio.Services.Content.Common.Telemetry;
using Microsoft.VisualStudio.Services.BlobStore.Common.Telemetry;

namespace Microsoft.VisualStudio.Services.Agent.Blob
{
    public class BlobStoreClientTelemetryTfs : BlobStoreClientTelemetry
    {
        private CustomerIntelligenceTelemetrySender _ciSender;

        public BlobStoreClientTelemetryTfs(IAppTraceSource tracer, Uri baseAddress, VssConnection connection) 
            : base(tracer, baseAddress)
        {
            _ciSender = new CustomerIntelligenceTelemetrySender(connection);
            this.senders.Add(_ciSender);
        }

        // for testing
        public BlobStoreClientTelemetryTfs(IAppTraceSource tracer, Uri baseAddress, VssConnection connection, ITelemetrySender sender)
            : base(tracer, baseAddress, sender)
        {
            _ciSender = new CustomerIntelligenceTelemetrySender(connection);
            this.senders.Add(_ciSender);
        }

        public async Task CommitTelemetry(Guid planId, Guid jobId)
        {
            await _ciSender.CommitTelemetry(planId, jobId);
        }
    }
}