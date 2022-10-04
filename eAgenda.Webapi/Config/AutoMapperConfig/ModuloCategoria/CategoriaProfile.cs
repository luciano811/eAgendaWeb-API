using AutoMapper;
using eAgenda.Dominio.Compartilhado;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webapi.ViewModels.ModuloCategoria;

namespace eAgenda.Webapi.Config.AutoMapperConfig.ModuloCategoria
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            ConverterDeEntidadeParaViewModel();

            ConverterDeViewModelParaEntidade();
        }

        private void ConverterDeViewModelParaEntidade()
        {
            CreateMap<InserirCategoriaViewModel, Categoria>();

            CreateMap<EditarCategoriaViewModel, Categoria>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Categoria, ListarCategoriaViewModel>();

            CreateMap<Categoria, VisualizarCategoriaViewModel>();
        }
    }
}
