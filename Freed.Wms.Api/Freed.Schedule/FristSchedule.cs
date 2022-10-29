using Coravel.Invocable;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Freed.Schedule
{
    /// <summary>
    /// 定时任务调度
    /// </summary>
    public class FristSchedule : IInvocable
    {
        private ILogger<FristSchedule> _logger;

        public FristSchedule(ILogger<FristSchedule> logger)
        {
            _logger = logger;
        }

        public async Task Invoke()
        {
            await Task.Run(() =>
            {
                _logger.LogInformation($"启动定时任务调度:{DateTime.Now.ToString()}");
            });
        }
    }
}
