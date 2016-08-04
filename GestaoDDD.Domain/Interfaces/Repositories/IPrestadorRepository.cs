using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using GestaoDDD.Domain.Entities;
using System;

namespace GestaoDDD.Domain.Interfaces.Repositories
{
    public interface IPrestadorRepository : IRepositoryBase<Prestador>
    {
        Prestador GetPorCpf(string cpf);
        Prestador GetPorGuid(Guid guid);

        //retorna o prestador atraves do email
        Prestador GetPorEmail(string email);

        //retorna os pretadores que nao estao ligados ao orçamento selecionado
        IEnumerable<Prestador> GetPrestadores(int orcamentoId);
        IEnumerable<Prestador> GetPrestadoresComServicos();
    }
}
