using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 数据库初始化种子
    /// </summary>
    public sealed class InitData : CreateDatabaseIfNotExists<ReportDbContext>
    {
        protected override void Seed(ReportDbContext context)
        {
            //默认用户
            new List<User>
            {
                new User{
                UserName="mr.wangya@qq.com", 
                UserPwd= "670b14728ad9902aecba32e22fa4f6bd", 
                CreateTime = DateTime.Now, 
                IsEnable = true,
                Nick = "超级管理员"}
            }.ForEach(m => context.User.Add(m));

            new List<Domain>
            {
                new Domain { SiteDomain = "www.baidu.com", DomainAlias = "百度" }
            }.ForEach(m => context.Domain.Add(m));
        }
    }
}
