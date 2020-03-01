using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiFundamental.Data.Entities;
using WebApiFundamental.ViewModel;

namespace WebApiFundamental.Data
{
    public class CampMappingProfile:Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampViewModel>();
        }
    }
}