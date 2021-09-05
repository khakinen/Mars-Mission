using Microsoft.Extensions.DependencyInjection;

namespace RoverInTheMars.Services.Rovers
{
    public class RoverFactory : IRoverFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RoverFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Rover CreateRover()
        {
            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
            var provider = serviceScope.ServiceProvider;

            return provider.GetRequiredService<Rover>();
        }
    }
}
