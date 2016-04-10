using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.ExceptionServices;
using log4net;
using StackExchange.Redis;
using SmartAnalytics.Cache;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Task
{
    class Program
    {
        private readonly static string RedisConnection = ConfigurationManager.AppSettings["RedisConnection"];
        private static readonly List<ITaskService> TaskServiceList = new List<ITaskService>();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomainOnFirstChanceException;

            if (Init() == false) return;

            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                PrintCommand();
                return;
            }

            var argsCommand = args[0];

            foreach (var m in TaskServiceList)
            {
                if (string.Compare(m.Command, argsCommand, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    try
                    {
                        var otherArgs = args.Skip(1).ToArray();

                        m.Exec(otherArgs);

                        return;
                    }
                    catch (Exception e)
                    {
                        Logger.Error(string.Format("命令：{0}\n异常{1}", m.Command, e.Message));
                    }
                }
            }

            Console.WriteLine("{0}命令无效", argsCommand);

            PrintCommand();
        }

        private static void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            //Logger.Error(e.Exception);
        }

        private static void PrintCommand()
        {
            Console.WriteLine();

            Console.WriteLine("{0,-18}\t描述", "命令");

            foreach (var m in TaskServiceList)
            {
                Console.WriteLine("{0,-18}\t{1}", m.Command, m.Name);
            }

            Console.WriteLine();
        }

        private static bool Init()
        {
            try
            {
                if (RedisContext.RedisDatabase == null)
                {
                    Logger.Info("初始化Redis Connection");
                    RedisContext.RedisDatabase = ConnectionMultiplexer.Connect(RedisConnection).GetDatabase();
                }

                Logger.Info("初始化Command List");

                //流量分析
                TaskServiceList.Add(new TimeSpanByDayTaskService());
                TaskServiceList.Add(new TimeSpanByHourTaskService());
                TaskServiceList.Add(new TimeSpanByMinuteTaskService());

                //来源分析
                TaskServiceList.Add(new OriginCategoryByHourTaskService());
                TaskServiceList.Add(new OriginPageByHourTaskService());
                TaskServiceList.Add(new OriginWordByDayTaskService());

                //访客分析
                TaskServiceList.Add(new VisitorRegionByDayTaskService());
                TaskServiceList.Add(new VisitorActiveByDayTaskService());
                TaskServiceList.Add(new VisitorLoyaltyByDayTaskService());
                TaskServiceList.Add(new NewOldVisitorByDayTaskService());
                TaskServiceList.Add(new VisitorResolutionByDayTaskService());

                //受访分析
                TaskServiceList.Add(new SurveyDomainByHourTaskService());
                //受访页面
                TaskServiceList.Add(new SurveyPageByDayTaskService());


                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);

                return false;
            }
        }
    }
}
