using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 统计报表数据库
    /// 
    /// 基础表：
    /// 用户表、统计域名表、IP区域表、浏览器表、网站域名分类表（保留）、关键词表（保留）、
    /// 
    /// 流量分析：
    /// 按小时流量统计表、按日流量统计表
    /// 
    /// 来源分析：
    /// 
    /// 访客分析：
    /// 
    /// </summary>
    public class ReportDbContext : DbContext
    {
        #region 基础表

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// 浏览器表
        /// </summary>
        public DbSet<Browse> Browse { get; set; }

        /// <summary>
        /// 统计域名表
        /// </summary>
        public DbSet<Domain> Domain { get; set; }

        /// <summary>
        /// IP区域表
        /// </summary>
        public DbSet<IpAddressArea> IpAddressArea { get; set; }

        /// <summary>
        /// 县及县以上行政区划代码
        /// </summary>
        public DbSet<CityAreaCode> CityAreaCode { get; set; }

        /// <summary>
        /// 网站分类表
        /// </summary>
        public DbSet<SiteCategory> SiteCategory { get; set; }

        /// <summary>
        /// 行业代码
        /// </summary>
        public DbSet<IndustryCode> IndustryCode { get; set; }

        #endregion

        #region 流量分析

        /// <summary>
        /// 按日流量统计表
        /// </summary>
        public DbSet<FlowVolumeByDay> FlowVolumeByDay { get; set; }

        /// <summary>
        /// 按小时流量统计表
        /// </summary>
        public DbSet<FlowVolumeByHour> FlowVolumeByHour { get; set; }

        /// <summary>
        /// 按小时流量统计表(预测数据)
        /// </summary>
        public DbSet<PredictFlowVolumeByHour> PredictFlowVolumeByHour { get; set; }

        /// <summary>
        /// 按分钟流量统计表
        /// </summary>
        public DbSet<FlowVolumeByMinute> FlowVolumeByMinute { get; set; }

        #endregion

        #region 来源分析

        /// <summary>
        /// 来源分类
        /// </summary>
        public DbSet<OriginCategory> OriginCategory { get; set; }

        /// <summary>
        /// 搜索词
        /// </summary>
        public DbSet<OriginWord> OriginWord { get; set; }

        /// <summary>
        /// 来源页面
        /// </summary>
        public DbSet<OriginPage> OriginPage { get; set; }

        #endregion

        #region 访客分析

        /// <summary>
        /// 地域分布
        /// </summary>
        public DbSet<VisitorRegion> VisitorRegion { get; set; }

        /// <summary>
        /// 访客分析-系统环境-浏览器
        /// </summary>
        public DbSet<VisitorBrowse> VisitorBrowse { get; set; }

        /// <summary>
        /// 访客分析-系统环境-浏览器内核
        /// </summary>
        public DbSet<VisitorBrowseKernel> VisitorBrowseKernel { get; set; }

        /// <summary>
        /// 访客分析-系统环境-语言
        /// </summary>
        public DbSet<VisitorLanguage> VisitorLanguage { get; set; }

        /// <summary>
        /// 访客分析-系统环境-操作系统
        /// </summary>
        public DbSet<VisitorOperatingSystem> VisitorOperatingSystem { get; set; }

        /// <summary>
        /// 访客分析-系统环境-分辨率
        /// </summary>
        public DbSet<VisitorResolution> VisitorResolution { get; set; }

        /// <summary>
        /// 访客分析-系统环境-终端类型
        /// </summary>
        public DbSet<VisitorTerminal> VisitorTerminal { get; set; }

        /// <summary>
        /// 访客分析-访客忠诚度-访问深度分布
        /// </summary>
        public DbSet<VisitorActive> VisitorActive { get; set; }

        /// <summary>
        /// 访客分析-访客忠诚度-日访问频度
        /// </summary>
        public DbSet<VisitorLoyalty> VisitorLoyalty { get; set; }

        /// <summary>
        /// 访客分析-新老访客
        /// </summary>
        public DbSet<VisitorNewOld> VisitorNewOld { get; set; }

        #endregion

        #region 受访分析

        /// <summary>
        /// 网址列表
        /// </summary>
        public DbSet<UrlMap> UrlMap { get; set; }

        /// <summary>
        /// 受访域名
        /// </summary>
        public DbSet<SurveyDomain> SurveyDomain { get; set; }

        /// <summary>
        /// 受访页面
        /// </summary>
        public DbSet<SurveyPage> SurveyPage { get; set; }

        #endregion
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除复数表名
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}