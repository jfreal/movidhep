using AutoMapper;
using Movid.Shared.Model;

namespace Movid.Marketing.Application
{
    public class AutoMappingConfigurations
    {
        public static void Bootstrap()
        {
            Mapper.CreateMap<BlogEntry, BlogEntryViewModel>();


            Mapper.AssertConfigurationIsValid();
        }
    }
}