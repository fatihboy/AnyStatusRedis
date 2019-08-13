﻿using AnyStatus.API;
using AnyStatus.Plugins.Redis.Shared;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AnyStatus.Plugins.Redis.UsedMemory
{
    public class UsedMemoryHandler : IRequestHandler<MetricQueryRequest<UsedMemoryWidget>>
    {
        public async Task Handle(MetricQueryRequest<UsedMemoryWidget> request, CancellationToken cancellationToken)
        {
            var clientListWidget = request.DataContext;

            var multiplexer = RedisHelper.GetConnectionMultiplexer(clientListWidget);
            var redisServer = multiplexer.GetServer(clientListWidget.EndPoint);

            var info = await redisServer.InfoAsync("memory");

            var usedMemory = double.Parse(info[0].First(stat => stat.Key == "used_memory").Value);
            var totalSystemMemory = double.Parse(info[0].First(stat => stat.Key == "total_system_memory").Value);
            var usedMemoryHuman = info[0].First(stat => stat.Key == "used_memory_human").Value;
            var totalSystemMemoryHuman = info[0].First(stat => stat.Key == "total_system_memory_human").Value;

            request.DataContext.Progress = (int)Math.Round((usedMemory / (double)totalSystemMemory) * 100);
            request.DataContext.Message = $"Used {request.DataContext.Progress}%{Environment.NewLine}" +
                   $"{usedMemoryHuman} used out of {totalSystemMemoryHuman}";

            request.DataContext.State = request.DataContext.Progress >= request.DataContext.ErrorPercentage ? State.Failed : State.Ok;
        }
    }
}
