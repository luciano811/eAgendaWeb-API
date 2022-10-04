using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.Controllers.Compartilhado;
using eAgenda.Webapi.ViewModels.ModuloCategoria;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : eAgendaControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private IMapper mapeadorCategorias;

        public CategoriasController(IMapper mapeadorCategorias, ServicoCategoria servicoCategoria)
        {
            this.mapeadorCategorias = mapeadorCategorias;
            this.servicoCategoria = servicoCategoria;
        }

        [HttpGet] //Action, Ação do Controlador, Endpoint
        public ActionResult<List<ListarCategoriaViewModel>> SelecionarTodos()
        {
            var categoriaResult = servicoCategoria.SelecionarTodos();
            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);
            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<List<ListarCategoriaViewModel>>(categoriaResult.Value)
            });
        }


        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarCategoriaViewModel> SelecionarCategoriaCompletaPorId(Guid id)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);
            if (categoriaResult.IsFailed && RegistroNaoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<VisualizarCategoriaViewModel>(categoriaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCategoriaViewModel> Inserir(InserirCategoriaViewModel categoriaVM)
        {
            var categoria = mapeadorCategorias.Map<Categoria>(categoriaVM);
            var categoriaResult = servicoCategoria.Inserir(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpPut("{id:guid}")]

        public ActionResult<FormsCategoriaViewModel> Editar(Guid id, EditarCategoriaViewModel categoriaVM)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);
            if (categoriaResult.IsFailed && RegistroNaoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            var categoria = mapeadorCategorias.Map(categoriaVM, categoriaResult.Value);
            categoriaResult = servicoCategoria.Editar(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });

        }


        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var categoriaResult = servicoCategoria.Excluir(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado<Categoria>(categoriaResult))
                return NotFound(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError<Categoria>(categoriaResult);

            return NoContent();
        }
    }
}