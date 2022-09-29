using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using System;
using System.Collections.Generic;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class VisualizarCompromissoViewModel
    {
        public VisualizarCompromissoViewModel()
        {
        }

        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }
        public DateTime Data { get; set; }
        public Contato Contato { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }

    }
}