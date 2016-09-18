using System;
using System.Xml;

namespace QuartzScheduler_Windows_Services.Net
{
    public class Common
    {
        public static string ConfigFilePath = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, "Configuration.xml");

        /// <summary>
        /// This method uses to get the configuration from xml file.
        /// </summary>
        /// <returns></returns>
        public static ConfigurationModel GetConfiguration()
        {
            log4net.Config.XmlConfigurator.Configure();
            var loaderConfig = new ConfigurationModel();

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigFilePath);
            var root = xmlDoc.SelectSingleNode("config");
            if (root == null) return loaderConfig;

            #region Get configured info from xml
            var chienvhInfo = root.SelectSingleNode("chienvh");
            if (chienvhInfo != null)
            {
                //set current node point to scheduleInterval
                var node = chienvhInfo.SelectSingleNode("scheduleInterval");

                var scheduler = chienvhInfo.SelectSingleNode("scheduleInterval");
                if (scheduler != null)
                {
                    node = scheduler.SelectSingleNode("days");
                    if (node != null)
                    {
                        int days;
                        Int32.TryParse(node.InnerText, out days);
                        loaderConfig.ScheduleIntervalInDays = days;
                    }

                    node = scheduler.SelectSingleNode("hours");
                    if (node != null)
                    {
                        int hours;
                        Int32.TryParse(node.InnerText, out hours);
                        loaderConfig.ScheduleIntervalInHours = hours;
                    }

                    node = scheduler.SelectSingleNode("minutes");
                    if (node != null)
                    {
                        int minutes;
                        Int32.TryParse(node.InnerText, out minutes);
                        loaderConfig.ScheduleIntervalInMinutes = minutes;
                    }

                    node = scheduler.SelectSingleNode("seconds");
                    if (node != null)
                    {
                        int seconds;
                        Int32.TryParse(node.InnerText, out seconds);
                        loaderConfig.ScheduleIntervalInSeconds = seconds;
                    }

                    node = scheduler.SelectSingleNode("startAtHours");
                    if (node != null)
                    {
                        int startAtHours;
                        Int32.TryParse(node.InnerText, out startAtHours);
                        loaderConfig.ScheduleStartAtHours = startAtHours;
                    }

                    node = scheduler.SelectSingleNode("startAtMinutes");
                    if (node != null)
                    {
                        int startAtMinutes;
                        Int32.TryParse(node.InnerText, out startAtMinutes);
                        loaderConfig.ScheduleStartAtMinutes = startAtMinutes;
                    }

                    var startAtDate = scheduler.SelectSingleNode("startAtDate");
                    if (startAtDate != null)
                    {
                        DateTime outStartAtDate;
                        DateTime.TryParse(startAtDate.InnerText, out outStartAtDate);
                        loaderConfig.ScheduleStartAtDate = outStartAtDate;
                    }
                }
            }

            #endregion

            return loaderConfig;
        }
    }
}
