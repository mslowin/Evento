using Evento.Evento.Core.Domain;

namespace Evento.Evento.Infrastructure.DTO
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public DateTime StartDate { get;  set; }
        public DateTime EndDate { get;  set; }
        public DateTime UpdatedAt { get;  set; }
        public int TicketsCount { get; set; }
    }
}
