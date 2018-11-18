using System.Threading.Tasks;
using EACA_API.Models.Schedule;

namespace EACA_API.Controllers.ScheduleApi.Services
{
    public interface IScheduleService
    {
        GroupsList GetGroupsList();
        ParityWeeks GetParityWeeks();
        Task<Schedule> GetSchedule(int groupId, string parity);
    }
}
