using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace eAgenda.Webapi.Filters
{
    public class ValidarViewModelActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var listaErros = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage);

                context.Result = new BadRequestObjectResult(new
                {
                    sucesso = false,
                    erros = listaErros.ToList()
                });

                return; //pra sair caso dê o erro né

            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

    }
}
