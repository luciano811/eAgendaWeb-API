using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.Controllers.Compartilhado;
using eAgenda.Webapi.ViewModels;
using eAgenda.Webapi.ViewModels.ModuloDespesa;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : eAgendaControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private IMapper mapeadorDespesas;

        public DespesasController(IMapper mapeadorDespesas, ServicoDespesa servicoDespesa)
        {
            this.mapeadorDespesas = mapeadorDespesas;
            this.servicoDespesa = servicoDespesa;
        }

        [HttpGet] //Action, Ação do Controlador, Endpoint
        public ActionResult<List<ListarDespesaViewModel>> SelecionarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();
            if (despesaResult.IsFailed)
                return InternalError(despesaResult);
            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesas.Map<List<ListarDespesaViewModel>>(despesaResult.Value)
            });
        }


        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarDespesaViewModel> SelecionarDespesaCompletaPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);
            if (despesaResult.IsFailed && RegistroNaoEncontrado(despesaResult))
                return NotFound(despesaResult);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesas.Map<VisualizarDespesaViewModel>(despesaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsDespesaViewModel> Inserir(InserirDespesaViewModel despesaVM)
        {
            var despesa = mapeadorDespesas.Map<Despesa>(despesaVM);
            var despesaResult = servicoDespesa.Inserir(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });
        }

        [HttpPut("{id:guid}")]

        public ActionResult<FormsDespesaViewModel> Editar(Guid id, EditarDespesaViewModel despesaVM)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);
            if (despesaResult.IsFailed && RegistroNaoEncontrado(despesaResult))
                return NotFound(despesaResult);

            var despesa = mapeadorDespesas.Map(despesaVM, despesaResult.Value);
            despesaResult = servicoDespesa.Editar(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });

        }


        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var despesaResult = servicoDespesa.Excluir(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado<Despesa>(despesaResult))
                return NotFound(despesaResult);

            if (despesaResult.IsFailed)
                return InternalError<Despesa>(despesaResult);

            return NoContent();
        }


    }
}