using System;

namespace QuartzScheduler_Windows_Services.Net
{
    public class ConfigurationModel
    {
        public int ScheduleIntervalInDays { get; set; }
        public int ScheduleIntervalInHours { get; set; }
        public int ScheduleIntervalInMinutes { get; set; }
        public DateTime ScheduleStartAtDate { get; set; }
        public int ScheduleStartAtHours { get; set; }
        public int ScheduleStartAtMinutes { get; set; }
    }
}
