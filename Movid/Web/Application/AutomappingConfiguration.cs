using AutoMapper;
using Movid.App.Models.ViewModels;
using Movid.Shared;
using Movid.Shared.Model;

namespace Movid.App.Application
{
    public static class AutomappingConfiguration
    {
        public static void Bootstrap()
        {
            Mapper.CreateMap<Exercise, ExerciseViewModel>();

        }
    }
}