﻿using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Interfaces.Repositories;
using GestaoDDD.Infra.Data.Contexto;

namespace GestaoDDD.Infra.Data.Repositories
{
    public class LogRepository : RepositoryBase<Log>, ILogRepository
    {
        private readonly GestaoContext _dbContext;

        public LogRepository(GestaoContext dbContext)
            :base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
