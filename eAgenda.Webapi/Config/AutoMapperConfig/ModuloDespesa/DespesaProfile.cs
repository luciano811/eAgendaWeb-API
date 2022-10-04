using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.ViewModels;
using eAgenda.Webapi.ViewModels.ModuloDespesa;

namespace eAgenda.Webapi.Config.AutoMapperConfig.ModuloDespesa
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            ConverterDeEntidadeParaViewModel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirDespesaViewModel, Despesa>();


            CreateMap<EditarDespesaViewModel, Despesa>();

        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Despesa, ListarDespesaViewModel>()
                    .ForMember(destino => destino.FormaPagamento, opt => opt.MapFrom(origem => origem.FormaPagamento.GetDescription()));




            CreateMap<Despesa, VisualizarDespesaViewModel>()
                    .ForMember(destino => destino.FormaPagamento, opt => opt.MapFrom(origem => origem.FormaPagamento.GetDescription()));

        }
    }
}
