using Shared;

namespace SeverGrpc_NHibernate.Service
{
    public class MyService : IMyService
    {
        public Task<MyServiceResult> DoSomething(MyServiceRequest request)
        {
            return Task.FromResult(new MyServiceResult()
            {
                NewText = request.Text + " from server",
                NewValue = request.Value + 1
            });
        }
    }
}
