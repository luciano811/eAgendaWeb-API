using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webapi.ViewModels.ModuloCompromisso;

namespace eAgenda.Webapi.Config.AutoMapperConfig.ModuloCompromisso
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            ConverterDeEntidadeParaViewModel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirCompromissoViewModel, Compromisso>();
            //.ForMember(destino => destino.Contato, opt => opt.Ignore())
            //            .AfterMap((viewModel, compromisso) =>
            //            {                               
            //                var contato = new Contato();
            //                contato = compromisso.Contato;

            //            });

            CreateMap<EditarCompromissoViewModel, Compromisso>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Compromisso, ListarCompromissoViewModel>();

            CreateMap<Compromisso, VisualizarCompromissoViewModel>();
        }
    }
}
