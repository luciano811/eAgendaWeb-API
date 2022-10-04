using eAgenda.Dominio.ModuloTarefa;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webapi.ViewModels.ModuloCategoria
{
    public class FormsCategoriaViewModel
    {
        [Required(ErrorMessage = "O '{0}' é obrigatório")]
        public string Descricao { get; set; }    

    }

    public class InserirCategoriaViewModel : FormsCategoriaViewModel
    {
    }
    public class EditarCategoriaViewModel : FormsCategoriaViewModel
    {
    }
}