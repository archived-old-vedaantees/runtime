using Vedaantees.Framework.Shell.Modules;
using Vedaantees.Framework.Types.Results;

namespace Vedaantees.Hosts.Api
{
    public class RunAfterLoadingIsComplete : IRunAfterLoadingIsComplete
    {
       
        public RunAfterLoadingIsComplete()
        {

        }

        public MethodResult Run()
        {
            return new MethodResult(MethodResultStates.Successful);
        }

        public LoadingExecutionPriority GetPriority()
        {
            return LoadingExecutionPriority.First;
        }
    }
}