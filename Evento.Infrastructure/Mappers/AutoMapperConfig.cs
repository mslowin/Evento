using AutoMapper;
using Evento.Evento.Core.Domain;
using Evento.Evento.Infrastructure.DTO;

namespace Evento.Evento.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDto>()
                    .ForMember(x => x.TicketsCount, m => m.MapFrom(p => p.Tickets.Count()));
                cfg.CreateMap<Event, EventDetailsDto>();
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<User, AccountDto>();
            })
            .CreateMapper();
    }
}
