﻿using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using System;

namespace GestaoDDD.Domain.Interfaces.Services
{
    public interface IPrestadorService : IServiceBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
        Prestador GetPorGuid(Guid guid);

        //retorna o prestador atraves do email
        Prestador GetPorEmail(string email);

        //retorna os pretadores que nao estao ligados ao orçamento selecionado
        IEnumerable<Prestador> GetPrestadores(int orcamentoId);
        IEnumerable<Prestador> GetPrestadoresComServicos();

        //retorna todos os prestadores ativos
        IEnumerable<Prestador> RetornaPrestadoresAtivos();
        IEnumerable<Prestador> GetAdministradores();
    }
}
