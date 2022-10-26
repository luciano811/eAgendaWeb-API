using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Webapi.ViewModels.ModuloContato;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ContatosController : eAgendaControllerBase
    {
        private readonly ServicoContato servicoContato;
        private IMapper mapeadorContatos;

        public ContatosController(IMapper mapeadorContatos, ServicoContato servicoContato)
        {
            this.mapeadorContatos = mapeadorContatos;
            this.servicoContato = servicoContato;
        }

        [HttpGet] //Action, Ação do Controlador, Endpoint
        public ActionResult<List<ListarContatoViewModel>> SelecionarTodos()
        {
            var contatoResult = servicoContato.SelecionarTodos();
            if (contatoResult.IsFailed)
                return InternalError(contatoResult);
            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<List<ListarContatoViewModel>>(contatoResult.Value)
            });
        }


        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarContatoViewModel> SelecionarContatoCompletaPorId(Guid id)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);
            if (contatoResult.IsFailed && RegistroNaoEncontrado(contatoResult))
                return NotFound(contatoResult);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<VisualizarContatoViewModel>(contatoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsContatoViewModel> Inserir(InserirContatoViewModel contatoVM)
        {
            var contato = mapeadorContatos.Map<Contato>(contatoVM);
            contato.UsuarioId = UsuarioLogado.Id;
            var contatoResult = servicoContato.Inserir(contato);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVM
            });
        }

        [HttpPut("{id:guid}")]

        public ActionResult<FormsContatoViewModel> Editar(Guid id, EditarContatoViewModel contatoVM)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);
            if (contatoResult.IsFailed && RegistroNaoEncontrado(contatoResult))
                return NotFound(contatoResult);

            var contato = mapeadorContatos.Map(contatoVM, contatoResult.Value);
            contatoResult = servicoContato.Editar(contato);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVM
            });

        }


        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var contatoResult = servicoContato.Excluir(id);

            if (contatoResult.IsFailed && RegistroNaoEncontrado<Contato>(contatoResult))
                return NotFound(contatoResult);

            if (contatoResult.IsFailed)
                return InternalError<Contato>(contatoResult);

            return NoContent();
        }     
    }
}