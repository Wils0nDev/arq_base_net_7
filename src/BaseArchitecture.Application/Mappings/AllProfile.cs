using AutoMapper;
using BaseArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseArchitecture.Application.Mappings
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            //Obtenemos todas las clases del proyecto actual
            //var dll = Assembly.GetExecutingAssembly();
            //var dll = Assembly.Load("BaseArchitecture.Domain");
            var dll = typeof(IGenerateEntity<>).Assembly;

            //obtenemos todas las clases Entidades.
            //var types = dll.GetTypes().Where(x => x.Name.Contains("Be")).ToList();
            var types = dll.GetTypes().ToList();

            types.ForEach(x =>
            {
                //creamos el mapeo para las convercion de objetos
                CreateMap(x, x);
            });
        }


    }

}
