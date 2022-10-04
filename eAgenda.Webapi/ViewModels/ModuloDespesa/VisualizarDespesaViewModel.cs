using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloContato;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class VisualizarDespesaViewModel
    {
        public VisualizarDespesaViewModel()
        {
        }

        public Guid Id { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<Categoria> Categorias { get; set; }

    }
}