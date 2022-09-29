using AutoMapper;
using eAgenda.Aplicacao.ModuloCompromisso;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Webapi.Controllers.Compartilhado;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompromissosController : eAgendaControllerBase
    {
        private readonly ServicoCompromisso servicoCompromisso;
        private IMapper mapeadorCompromissos;

        public CompromissosController(IMapper mapeadorCompromissos, ServicoCompromisso servicoCompromisso)
        {
            this.mapeadorCompromissos = mapeadorCompromissos;
            this.servicoCompromisso = servicoCompromisso;
        }

        [HttpGet] //Action, Ação do Controlador, Endpoint
        public ActionResult<List<ListarCompromissoViewModel>> SelecionarTodos()
        {
            var compromissoResult = servicoCompromisso.SelecionarTodos();
            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);
            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<List<ListarCompromissoViewModel>>(compromissoResult.Value)
            });
        }


        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarCompromissoViewModel> SelecionarCompromissoCompletaPorId(Guid id)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);
            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult))
                return NotFound(compromissoResult);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCompromissos.Map<VisualizarCompromissoViewModel>(compromissoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCompromissoViewModel> Inserir(InserirCompromissoViewModel compromissoVM)
        {
            var compromisso = mapeadorCompromissos.Map<Compromisso>(compromissoVM);
            var compromissoResult = servicoCompromisso.Inserir(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });
        }

        [HttpPut("{id:guid}")]

        public ActionResult<FormsCompromissoViewModel> Editar(Guid id, EditarCompromissoViewModel compromissoVM)
        {
            var compromissoResult = servicoCompromisso.SelecionarPorId(id);
            if (compromissoResult.IsFailed && RegistroNaoEncontrado(compromissoResult))
                return NotFound(compromissoResult);

            var compromisso = mapeadorCompromissos.Map(compromissoVM, compromissoResult.Value);
            compromissoResult = servicoCompromisso.Editar(compromisso);

            if (compromissoResult.IsFailed)
                return InternalError(compromissoResult);

            return Ok(new
            {
                sucesso = true,
                dados = compromissoVM
            });

        }


        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var compromissoResult = servicoCompromisso.Excluir(id);

            if (compromissoResult.IsFailed && RegistroNaoEncontrado<Compromisso>(compromissoResult))
                return NotFound(compromissoResult);

            if (compromissoResult.IsFailed)
                return InternalError<Compromisso>(compromissoResult);

            return NoContent();
        }
    }
}