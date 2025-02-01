using AutoMapper;
using SGA.Models;
using SGA.ViewModels;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Student, StudentViewModel>().ReverseMap();
  }
}