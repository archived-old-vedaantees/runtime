#region usings

using System.Threading.Tasks;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Hosts.SingleSignOn.Domain.Commands;
using Vedaantees.Framework.Providers.Storages.Graphs;

#endregion

namespace Vedaantees.Hosts.SingleSignOn.Domain.Handlers
{
    public class GraphHandler : ICommandHandler<RegisterUserCommand>,
                                ICommandHandler<VerifyUserEmailCommand>
    {
        private readonly IGraphStore _graphStore;

        public GraphHandler(IGraphStore graphStore)
        {
            _graphStore = graphStore;
        }

        public Task Handle(RegisterUserCommand command)
        {
            _graphStore.Execute($"CREATE (:User {{ Id:'{command.Id}', isActive:false }})");
            return Task.CompletedTask;
        }

        public Task Handle(VerifyUserEmailCommand command)
        {
            _graphStore.Execute($"MATCH (u:User {{ Id:'{command.Id}'}}) SET u.isActive = true RETURN u");
            return Task.CompletedTask;
        }
    }
}