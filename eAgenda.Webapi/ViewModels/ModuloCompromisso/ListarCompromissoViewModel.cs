using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using System;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class ListarCompromissoViewModel //ver a questao do CONTATO
    {
        public Guid Id { get; set; }
        public string Assunto { get; set; }
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }
        public string Local { get; set; }
        public string Link { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Guid ContatoId { get; set; }


    }
}