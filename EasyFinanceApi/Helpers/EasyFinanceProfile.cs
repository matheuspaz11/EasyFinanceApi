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
            CreateMap<Expense, GetExpenseDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(scr => EnumHelper.GetEnumDescription(scr.Status)))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(scr => EnumHelper.GetEnumDescription(scr.Type)))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate.Value.ToLocalTime().ToString("yyyy-MM-dd")));

            CreateMap<Expense, ChangeExpenseDTO>();
            CreateMap<ChangeExpenseDTO, Expense>();
        }
    }
}
