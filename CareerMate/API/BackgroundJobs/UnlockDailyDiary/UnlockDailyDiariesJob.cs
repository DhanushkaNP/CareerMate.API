using CareerMate.EndPoints.Commands.DailyDiaries.Unlock;
using MediatR;
using Quartz;
using System.Threading.Tasks;

namespace CareerMate.API.BackgroundJobs.UnlockDailyDiary
{
    public class UnlockDailyDiariesJob : IJob
    {
        private readonly IMediator _mediator;

        public UnlockDailyDiariesJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new UnlockDailyDiariesCommand());
        }
    }
}
