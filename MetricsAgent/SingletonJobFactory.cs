using Quartz;
using Quartz.Spi;

public class SingletonJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;
    public SingletonJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    }
    public void ReturnJob(IJob job) { }
}
