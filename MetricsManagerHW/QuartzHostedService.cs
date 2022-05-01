﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsManagerHW.Interface;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;

//namespace MetricsManagerHW;

public class QuartzHostedService : IHostedService
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;
    private readonly IEnumerable<JobSchedule> _jobSchedules;
    public IScheduler Scheduler { get; set; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
    public QuartzHostedService(ISchedulerFactory schedulerFactory,
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
                               IJobFactory jobFactory,
                               IEnumerable<JobSchedule> jobSchedules)
    {
        _schedulerFactory = schedulerFactory;
        _jobSchedules = jobSchedules;
        _jobFactory = jobFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        Scheduler.JobFactory = _jobFactory;
        foreach (var jobSchedule in _jobSchedules)
        {
            var job = CreateJobDetail(jobSchedule);
            var trigger = CreateTrigger(jobSchedule);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
        }
        await Scheduler.Start(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
        await Scheduler?.Shutdown(cancellationToken);
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
    }

    private static IJobDetail CreateJobDetail(JobSchedule schedule)
    {
        var jobType = schedule.JobType;
        return JobBuilder
        .Create(jobType)
        .WithIdentity(jobType.FullName!)
        .WithDescription(jobType.Name)
        .Build();
    }

    private static ITrigger CreateTrigger(JobSchedule schedule)
    {
        return TriggerBuilder
        .Create()
        .WithIdentity($"{schedule.JobType.FullName}.trigger")
        .WithCronSchedule(schedule.CronExpression)
        .WithDescription(schedule.CronExpression)
        .Build();
    }
}
