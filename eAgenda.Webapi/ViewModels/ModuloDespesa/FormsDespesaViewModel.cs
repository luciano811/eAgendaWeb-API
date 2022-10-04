using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webapi.ViewModels.ModuloDespesa
{
    public class FormsDespesaViewModel
    {
        [Required(ErrorMessage = "O '{0}' é obrigatório")]

        public string Descricao { get; set; }

        [Required(ErrorMessage = "O '{0}' é obrigatório")]

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<Categoria> Categorias { get; set; }


    }

    public class InserirDespesaViewModel : FormsDespesaViewModel
    {
    }
    public class EditarDespesaViewModel : FormsDespesaViewModel
    {
    }
}