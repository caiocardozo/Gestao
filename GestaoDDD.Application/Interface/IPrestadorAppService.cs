using System.Collections.Generic;
using GestaoDDD.Domain.Entities;
using System;

namespace GestaoDDD.Application.Interface
{
    public interface IPrestadorAppService : IAppServiceBase<Prestador>
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

        //verifica se o prestador ja esta cadastrado
        byte VeriricaPrestadorExiste(string email);
    }
}
