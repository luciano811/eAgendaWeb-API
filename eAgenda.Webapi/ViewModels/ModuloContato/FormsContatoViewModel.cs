using eAgenda.Dominio.ModuloTarefa;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webapi.ViewModels.ModuloContato
{
    public class FormsContatoViewModel
    {
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Email { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Empresa { get; set; }


        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Cargo { get; set; }



    }

    public class InserirContatoViewModel : FormsContatoViewModel
    {
    }
    public class EditarContatoViewModel : FormsContatoViewModel
    {
    }
}