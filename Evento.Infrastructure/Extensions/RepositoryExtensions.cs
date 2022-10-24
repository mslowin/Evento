using Evento.Evento.Core.Domain;
using Evento.Evento.Core.Repositories;
using Evento.Evento.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Evento.Evento.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository repository, Guid id)
        {
            var @event = await repository.GetAsync(id);
            if (@event == null)
            {
                throw new Exception($"Event with id: '{id}' does not exist.");
            }

            return @event;
        }
    }
}
