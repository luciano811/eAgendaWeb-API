using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.ViewModels.ModuloCategoria
{
    public class VisualizarCategoriaViewModel
    {
        public VisualizarCategoriaViewModel()
        {
        }

        public Guid Id { get; set; }
        public string Descricao { get; set; }
        
    }
}