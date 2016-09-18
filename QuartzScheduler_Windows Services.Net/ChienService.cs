using System;
using System.ServiceProcess;
using log4net;
using Quartz;
using Quartz.Impl;

namespace QuartzScheduler_Windows_Services.Net
{
    partial class ChienService : ServiceBase
    {
        //Declare an instance for log4net
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        //Create variables 
        protected string ChienJob = "ChienRunScheduleJob";
        protected string ChienGroup = "ChienRunScheduleGroup";
        protected string ChienTrigger = "ChienRunScheduleTrigger";
        protected string ChienScheduleStartedMsg = "Chien's schedule has been started";
        protected string ChienScheduleStoppedMsg = "Chien's schedule has been stopped";
        protected static string ChienJobStartedMsg = "Chien's Job has been started";
        protected static string ChienJobStoppedMsg = "Chien's Job has been completed";

        public ChienService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //Auto configures log4net based on the application's configuration setting.
            log4net.Config.XmlConfigurator.Configure();
            //Add log message when job is started
            Log.Info(ChienScheduleStartedMsg);
            RunApp();
        }

        protected override void OnStop()
        {
            //Add log messages when job is stopped
            Log.Info(ChienScheduleStoppedMsg);
        }

        public void RunApp()
        {
            //Construct a scheduler factory  
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            //Get a scheduler, start the schedular before triggers or anything else  
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            //Create job 
            var job = JobBuilder.Create<ExecutingJobs>()
                        .WithIdentity(ChienJob, ChienGroup)
                        .Build();
            try
            {
                //Trigger the job to run now, and then repeat every time configured 
                var config = Common.GetConfiguration();

                //Get configured times from Configuration.xml
                var timeToRunInDays = config.ScheduleIntervalInDays * 24 * 60;
                var timeToRunInHours = config.ScheduleIntervalInHours * 60;
                var timeToRunInMinutes = config.ScheduleIntervalInMinutes;
                var timeToRunInSeconds = config.ScheduleIntervalInSeconds;
                var scheduleTime = timeToRunInDays + timeToRunInHours + timeToRunInMinutes + timeToRunInSeconds;
                var startAtHours = config.ScheduleStartAtHours;
                var startAtMinutes = config.ScheduleStartAtMinutes;
                var startAtSeconds = config.ScheduleStartAtSeconds;

                //Used trigger builder to create a job with scheduled times
                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(ChienTrigger, ChienGroup)
                    .StartAt(DateTime.Now.Date.AddHours(startAtHours)
                                              .AddMinutes(startAtMinutes)
                                              .AddSeconds(startAtSeconds))
                                              
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(scheduleTime).RepeatForever())
                    .Build();

                //Schedule the job using the job and trigger   
                sched.ScheduleJob(job, trigger);
            }
            catch (SchedulerException se)
            {
                Log.Error(se.Message);
            }
        }


        // Create a class to handle the executing jobs
        public class ExecutingJobs : IJob
        {
            void IJob.Execute(IJobExecutionContext context)
            {
                Log.Info(ChienJobStartedMsg);
                ExecutingTasks();
                Log.InfoFormat(ChienJobStoppedMsg);
            }

            // Add your code here to perform your business. In this demo, I just added a simple one to write the current datetime into log file.
            // You can base on this example to develop your purposes.
            private void ExecutingTasks()
            {
                try
                {                    
                    var now = DateTime.Now.ToString("yyyyMMdd-HHMMss");
                    Log.Info(String.Format("Executed job at {0}", now));
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }
        }
    }
}
