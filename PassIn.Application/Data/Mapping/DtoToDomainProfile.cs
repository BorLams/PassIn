using AutoMapper;
using PassIn.Application.Data.Dtos.Attendee.Request;
using PassIn.Application.Data.Dtos.CheckIn.Request;
using PassIn.Application.Data.Dtos.CheckIn.Response;
using PassIn.Application.Data.Dtos.Event.Request;
using PassIn.Application.Data.Dtos.Event.Response;
using PassIn.Domain.Entities;

namespace PassIn.Application.Data.Mapping;
public class DtoToDomainProfile : Profile
{
    public DtoToDomainProfile()
    {
        #region Request
        CreateMap<RequestEventDto, Event>();
        CreateMap<RequestRegisterAttendeeDto, Attendee>();
        CreateMap<RequestCheckInDto, CheckIn>();
        #endregion

        #region Response
        CreateMap<ResponseEventDto, Event>();
        CreateMap<ResponseCheckInDto, CheckIn>();
        #endregion
    }
}
