using System;
using System.Collections.Generic;
using System.Linq;
using GeoCoordinatePortable;
using GestaoDDD.Application.Interface;
using GestaoDDD.Domain.Entities;
using GestaoDDD.Domain.Entities.NoSql;
using GestaoDDD.Domain.Interfaces.Services;
using System.Globalization;

namespace GestaoDDD.Application.Services
{
    public class OrcamentoAppService : AppServiceBase<Orcamento>, IOrcamentoAppService
    {
        private readonly IOrcamentoService _orcamentoService;
        public OrcamentoAppService(IOrcamentoService orcamentoService)
            : base(orcamentoService)
        {
            _orcamentoService = orcamentoService;
        }

        //retorna os orcamentos 
        public IEnumerable<Orcamento> RetornaOrcamentos(int servico, string cidade, EnumEstados estado)
        {
            return _orcamentoService.RetornaOrcamentos(servico, cidade, estado);
        }


        public IEnumerable<Orcamento> RetornaOrcamentosAbertos()
        {
            return _orcamentoService.RetornaOrcamentosAbertos();
        }


        public IEnumerable<Orcamento> RetornarOrcamentosComDistanciaCalculada(string prestadorLatitude,
            string prestadorLongitude, string raio, string usuarioId)
        {
            var orcamentos = _orcamentoService.RetornaOrcamentosAbertos().ToList();
            var orcamentosPagos = GetOrcamentoPagosPeloPrestador(usuarioId).ToList();



            var orcamentosView = new List<Orcamento>();
            foreach (var orcamento in orcamentos)
            {

                if (orcamentosPagos.All(s => s.orc_Id != orcamento.orc_Id))
                {
                    var coord_orcamento = new GeoCoordinate();
                    coord_orcamento.Latitude = double.Parse(orcamento.orc_latitude.Replace(",", "."),
                        CultureInfo.InvariantCulture);
                    coord_orcamento.Longitude = double.Parse(orcamento.orc_longitude.Replace(",", "."),
                        CultureInfo.InvariantCulture);

                    var coordenada_prestador = new GeoCoordinate();
                    coordenada_prestador.Latitude = double.Parse(prestadorLatitude.Replace(",", "."),
                        CultureInfo.InvariantCulture);
                    coordenada_prestador.Longitude = double.Parse(prestadorLongitude.Replace(",", "."),
                        CultureInfo.InvariantCulture);

                    var distancia = (coordenada_prestador.GetDistanceTo(coord_orcamento) / 1000);


                    /* var endereco = prestador.pres_Endereco;
                    var partes = endereco.Split(',');
                    foreach (var parte in partes.Where(s => s.Contains("-")))
                    {

                        var separar = parte.Split('-');
                        var ufs = " AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA,PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO";
                        if (ufs.Contains(separar[1]))
                        {
                            prestador.Estado =
                                (EnumClass.EnumEstados)Enum.Parse(typeof(EnumClass.EnumEstados), separar[1]);
                            prestador.Cidade = separar[0];
                        }
                        else
                            continue;

                    }*/

                    var endereco = orcamento.orc_endereco;
                    var cidade = "";
                    var estado = new EnumAppEstados();
                    var partes = endereco.Split(',');
                    foreach (var parte in partes.Where(s => s.Contains("-")))
                    {

                        var separar = parte.Split('-');
                        var ufs = " AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT, MS, MG, PA,PB, PR, PE, PI, RJ, RN, RS, RO, RR, SC, SP, SE, TO";
                        if (ufs.Contains(separar[1]))
                        {
                            estado = (EnumAppEstados)Enum.Parse(typeof(EnumAppEstados), separar[1]);
                            cidade = separar[0];
                        }
                        else
                            continue;

                    }

                    orcamento.Distancia = Math.Round(distancia, 2).ToString() + " Km do seu negócio em " +
                                          estado +
                                          " - " + cidade + " ";

                    if (distancia <= double.Parse(raio))
                        orcamentosView.Add(orcamento);
                }
            }
            return orcamentosView;
        }

        public IEnumerable<Orcamento> GetOrcamentoPagosPeloPrestador(string usuarioId)
        {
            return _orcamentoService.GetOrcamentoPagosPeloPrestador(usuarioId);
        }

        public IEnumerable<Orcamento> RetornaOrcamentosPagos(int servico, string cidade, EnumEstados estado, string usuarioId)
        {
            return _orcamentoService.RetornaOrcamentosPagos(servico, cidade, estado, usuarioId);
        }

        //retorna o orçamento pelo id
        public Orcamento RetornaOrcamentoPorId(int id)
        {
            return _orcamentoService.RetornaOrcamentoPorId(id);
        }
    }
    public enum EnumAppEstados
    {
        AC,
        AL,
        AP,
        AM,
        BA,
        CE,
        DF,
        ES,
        GO,
        MA,
        MT,
        MS,
        MG,
        PA,
        PB,
        PR,
        PE,
        PI,
        RJ,
        RN,
        RS,
        RO,
        RR,
        SC,
        SP,
        SE,
        TO
    }


}

