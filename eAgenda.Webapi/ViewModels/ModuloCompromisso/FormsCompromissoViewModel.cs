using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webapi.ViewModels.ModuloCompromisso
{
    public class FormsCompromissoViewModel
    {
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Assunto { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public TipoLocalizacaoCompromissoEnum TipoLocal { get; set; }

        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Local { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Link { get; set; }


        [Required(ErrorMessage = "A '{0}' é obrigatório")]
        public DateTime Data { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public Guid ContatoId { get; set; }

        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public TimeSpan HoraTermino { get; set; }



    }

    public class InserirCompromissoViewModel : FormsCompromissoViewModel
    {
    }
    public class EditarCompromissoViewModel : FormsCompromissoViewModel
    {
    }
}