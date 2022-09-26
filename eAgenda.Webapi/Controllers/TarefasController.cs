using AutoMapper;
using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloTarefa;
using eAgenda.Webapi.Config.AutoMapperConfig;
using eAgenda.Webapi.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ServicoTarefa servicoTarefa;
        private IMapper mapeadorTarefas;

        public TarefasController(IMapper mapeadorTarefas, ServicoTarefa servicoTarefa)
        {
            this.mapeadorTarefas = mapeadorTarefas;
            this.servicoTarefa = servicoTarefa;

            //var config = new ConfiguracaoAplicacaoeAgenda();

            //var eAgendaDbContext = new eAgendaDbContext(config.ConnectionStrings);
            //var repositorioTarefa = new RepositorioTarefaOrm(eAgendaDbContext);
            //servicoTarefa = new ServicoTarefa(repositorioTarefa, eAgendaDbContext);

            //var autoMapperConfig = new MapperConfiguration(config =>
            //{
            //    config.AddProfile<TarefaProfile>();
            //});

            //mapeadorTarefas = autoMapperConfig.CreateMapper();
        }

        [HttpGet]
        public ActionResult <List<ListarTarefaViewModel >> SelecionarTodos()
        {
            var tarefaResult = servicoTarefa.SelecionarTodos(StatusTarefaEnum.Todos);
            if (tarefaResult.IsFailed) 
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message),
                });
            }
            return Ok(new
            {
                sucesso = true,
                dados = mapeadorTarefas.Map<List<ListarTarefaViewModel>>(tarefaResult.Value),
            });
        }

        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarTarefaViewModel> SelecionarTarefaCompletaPorId(Guid id) // D6E3F379-E6CE-4F6F-8C95-08DA9A4935DF
        {
            var tarefaResult = servicoTarefa.SelecionarPorId(id);
            if (tarefaResult.Errors.Any(x => x.Message.Contains("não encontrada")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                }); 
            };
            if (tarefaResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }
            return Ok(new
            {
                sucesso = true,
                dados = mapeadorTarefas.Map < VisualizarTarefaViewModel > (tarefaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsTarefaViewModel> Inserir(InserirTarefaViewModel tarefaVM)
        {
            var listaErros = ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage);

            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });
            }

            var tarefa = mapeadorTarefas.Map<Tarefa>(tarefaVM);
            var tarefaResult = servicoTarefa.Inserir(tarefa);

            if (tarefaResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

            return Ok(new
            {             
                sucesso = true,
                dados = tarefaVM
            });
        }

        [HttpPut("{id:guid}")]

        public ActionResult<FormsTarefaViewModel> Editar(Guid id, EditarTarefaViewModel tarefaVM)
        {
            var listaErros = ModelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage);
            
            if (listaErros.Any())
            {
                return BadRequest(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });
            }
            
            var tarefaResult = servicoTarefa.SelecionarPorId(id);
            
            if (tarefaResult.Errors.Any(x => x.Message.Contains("não encontrada")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }

            var tarefa = mapeadorTarefas.Map(tarefaVM, tarefaResult.Value);
            tarefaResult = servicoTarefa.Editar(tarefa);
            
            if (tarefaResult.IsFailed)
            {
                
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                    });

            }

            if (tarefaResult.IsSuccess)
                return tarefaVM;

            return null;
        }


        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var tarefaResult = servicoTarefa.Excluir(id);
            
            if (tarefaResult.Errors.Any(x => x.Message.Contains("não encontrada")))
            {
                return NotFound(new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }
            
            if (tarefaResult.IsFailed)
            {
                return StatusCode(500, new
                {
                    sucesso = false,
                    erros = tarefaResult.Errors.Select(x => x.Message)
                });
            }
            
            return NoContent();

        }

    }
}