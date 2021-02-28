using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TesteITG.Domain.Domain;
using TesteITG.Entity.Entity;

namespace TesteITG.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteModel, Cliente>();
            CreateMap<Cliente, ClienteModel>();
            CreateMap<Guid, int>();
            CreateMap<int, Guid>();
        }
    }
}
