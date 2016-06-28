using GestaoDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Interfaces.Services;

namespace GestaoDDD.Application.Services
{
    public class LogAppService : AppServiceBase<Log>, ILogAppService
    {
        private readonly ILogService _logService;

        public LogAppService(ILogService logService)
            : base(logService)
        {
            _logService = logService;
        }
    }
}
