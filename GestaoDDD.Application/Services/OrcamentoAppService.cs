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
                    var endereco = orcamento.orc_endereco;
                    var separar = endereco.Split(',');
                    var cidadeEstado = separar[1].Split('-');
                    orcamento.Distancia = Math.Round(distancia, 2).ToString() + " do seu negócio em " +
                                          cidadeEstado[0] +
                                          " - " + cidadeEstado[1] + " ";

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
    }
}
