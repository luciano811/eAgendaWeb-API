using eAgenda.Dominio.ModuloCompromisso;
using System;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class ListarCompromissoViewModel //pegar dominios contato
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        public string Local { get; set; }
        public string Link { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }

    }
}