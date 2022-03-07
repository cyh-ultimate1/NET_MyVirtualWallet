using AutoMapper;
using MyVirtualWallet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyVirtualWallet.WebAPI.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDTO>();
            CreateMap<AccountDetails, AccountDetailsDTO>();
            CreateMap<AccountTransaction, AccountTransactionDTO>()
                /*.ForMember(dest => dest.AccountDetails, opt => opt.MapFrom(src => src.AccountDetails))*/;
            CreateMap<AccountTransactionDTO, AccountTransaction>();
        }
    }
}
