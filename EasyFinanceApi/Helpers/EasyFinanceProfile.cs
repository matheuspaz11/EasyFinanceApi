using AutoMapper;
using EasyFinanceApi.Models.DTOs;
using EasyFinanceApi.Models.Entities;
using static EasyFinanceApi.Models.Enums.Enum;

namespace EasyFinanceApi.Helpers
{
    public class EasyFinanceProfile : Profile
    {
        public EasyFinanceProfile() {
            CreateMap<ExpenseDTO, Expense>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ExpenseStatus.Pending))
                .ForMember(dest => dest.PaymentDate, opt => opt.Ignore());

            CreateMap<Expense, ExpenseDTO>();
        }
    }
}
