using eAgenda.Aplicacao.ModuloTarefa;
using eAgenda.Dominio.ModuloTarefa;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloTarefa;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eAgenda.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        [HttpGet]        
        public List<Tarefa> SelecionarTodos()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();
            var eAgendaDBContext = new eAgendaDbContext(config.ConnectionStrings);
            var repositorioTarefa = new RepositorioTarefaOrm(eAgendaDBContext);
            var servicoTarefa = new ServicoTarefa(repositorioTarefa, eAgendaDBContext);
            var tarefaResult = servicoTarefa.SelecionarTodos(StatusTarefaEnum.Todos);
            
            if (tarefaResult.IsSuccess)
                return tarefaResult.Value;

            return null;
        }
    }
}
