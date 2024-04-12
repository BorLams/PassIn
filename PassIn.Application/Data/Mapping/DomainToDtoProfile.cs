using AutoMapper;
using PassIn.Application.Data.Dtos.Attendee.Response;
using PassIn.Application.Data.Dtos.CheckIn.Response;
using PassIn.Application.Data.Dtos.Event.Request;
using PassIn.Application.Data.Dtos.Event.Response;
using PassIn.Domain.Entities;

namespace PassIn.Application.Data.Mapping;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        #region Request
        CreateMap<Event, RequestEventDto>();
        #endregion

        #region Response
        CreateMap<Event, ResponseEventDto>()
            .ForMember(dto => dto.AttendeesAmount, config => config.MapFrom(e => e.Attendees.Count()));
        CreateMap<Event, ResponseRegisteredEventDto>();
        CreateMap<Attendee, ResponseAttendeeDto>();
        CreateMap<CheckIn, ResponseCheckInDto>();
        #endregion
    }
}
