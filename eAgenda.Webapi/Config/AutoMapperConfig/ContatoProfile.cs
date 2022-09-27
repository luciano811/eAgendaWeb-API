using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.ViewModels.ModuloContato;

namespace eAgenda.Webapi.Config.AutoMapperConfig
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            ConverterDeEntidadeParaViewModel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirContatoViewModel, Contato>();

            CreateMap<EditarContatoViewModel, Contato>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Contato, ListarContatoViewModel>();

            CreateMap<Contato, VisualizarContatoViewModel>();
        }
    }
}
