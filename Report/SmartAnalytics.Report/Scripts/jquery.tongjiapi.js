/*
 * jQuery SmartAnalytics api Plugin v1.0.0
 * 
 * Copyright 2015
 * Released under the LGPL license
 * author mr.wangya@qq.com
 */
(function ($) {

    //使用选项初始化插件
    $.fn.tongjiapi = function (options) {
        var opts = $.extend({}, this.defaults, options);
        return opts;
    };

    //默认选项
    $.fn.tongjiapi.defaults = {

        serverUrl: 'http://api.SmartAnalytics.com/',

        //hello
        hello: '/api/hello/',

        //流量分析-按小时统计
        timeSpanByHour: '/api/TimeSpanByHour/',

        //流量分析-按日统计
        timeSpanByDay: '/api/TimeSpanByDay/',

        //流量分析-流量趋势
        predictTimeSpanByHour: '/api/PredictTimeSpanByHour/',

        //流量分析-分钟线流量统计
        timeSpanByMinute: '/api/TimeSpanByMinute/',

        //来源分析-来源分类-按日统计
        originCategoryDay: '/api/OriginCategory/day/',

        //来源分析-来源分类-按小时统计
        originCategoryHour: '/api/OriginCategory/hour/',

        //来源分析-来源域名-按日统计
        originDomainDay: '/api/OriginDomain/day/',

        //来源分析-来源域名-按小时统计
        originDomainHour: '/api/OriginDomain/hour/',

        //来源分析-来源页面-按日统计
        originPageDay: '/api/OriginPage/day/',

        //来源分析-来源页面-按小时统计
        originPageHour: '/api/OriginPage/hour/',

        //访客分析-新老访客
        newOldVisitor: '/api/NewOldVisitor/',

        //访客分析-系统环境-分辨率
        visitorResolution: '/api/VisitorResolution/',

        //受访分析-受访域名
        surveyDomainByHour: '/api/SurveyDomainByHour/'
    };

    //hello
    $.fn.tongjiapi.Hello = function (callback) {

        asyncGetRequest(this.defaults.hello, {}, callback);

    };

    //流量分析-按小时统计
    $.fn.tongjiapi.TimeSpanByHour = function (domain, date, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.timeSpanByHour, param, callback);

    };

    //流量分析-按日统计
    $.fn.tongjiapi.TimeSpanByDay = function (domain, beginDate, endDate, callback) {

        var param = { domain: domain, beginDate: beginDate, endDate: endDate };

        asyncGetRequest(this.defaults.timeSpanByDay, param, callback);

    };

    //流量分析-流量趋势
    $.fn.tongjiapi.PredictTimeSpanByHour = function (domain, date, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.predictTimeSpanByHour, param, callback);

    };

    //流量分析-分钟线流量统计
    $.fn.tongjiapi.TimeSpanByMinute = function (domain, minute, callback) {

        var param = { domain: domain, minute: minute };

        asyncGetRequest(this.defaults.timeSpanByMinute, param, callback);

    };

    //来源分析-来源分类-按日统计
    $.fn.tongjiapi.OriginCategoryDay = function (date, domain, industryCode, callback) {

        var param = { domain: domain, date: date, industryCode: industryCode };

        asyncGetRequest(this.defaults.originCategoryDay, param, callback);

    };

    //来源分析-来源分类-按小时统计
    $.fn.tongjiapi.OriginCategoryHour = function (date, domain, industryCode, callback) {

        var param = { domain: domain, date: date, industryCode: industryCode };

        asyncGetRequest(this.defaults.originCategoryHour, param, callback);

    };

    //来源分析-来源域名-按日统计
    $.fn.tongjiapi.OriginDomainDay = function (date, domain, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.originDomainDay, param, callback);

    };

    //来源分析-来源域名-按小时统计
    $.fn.tongjiapi.OriginDomainHour = function (date, domain, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.originDomainHour, param, callback);

    };

    //来源分析-来源页面-按日统计
    $.fn.tongjiapi.OriginPageDay = function (date, domain, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.originPageDay, param, callback);

    };

    //来源分析-来源页面-按小时统计
    $.fn.tongjiapi.OriginPageHour = function (date, domain, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.originPageHour, param, callback);

    };

    //访客分析-新老访客
    $.fn.tongjiapi.NewOldVisitor = function (date, domain, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.newOldVisitor, param, callback);

    };

    //访客分析-系统环境-分辨率
    $.fn.tongjiapi.VisitorResolution = function (date, domain, callback) {

        var param = { domain: domain, date: date };

        asyncGetRequest(this.defaults.visitorResolution, param, callback);

    };

    //受访分析-受访域名
    $.fn.tongjiapi.SurveyDomainByHour = function (date, callback) {

        var param = { date: date };

        asyncGetRequest(this.defaults.surveyDomainByHour, param, callback);

    };

    //异步GET请求
    function asyncGetRequest(url, param, callback) {

        syncRequest(url, 'get', param, callback);

    };

    //同步GET请求
    function syncGetRequest(url, param, callback) {

        asyncRequest(url, 'get', param, callback);

    };

    //异步POST请求
    function asyncPostRequest(url, param, callback) {

        asyncRequest(url, 'post', param, callback);

    };

    //异步Delete请求
    function asyncDeleteRequest(url, param, callback) {

        asyncRequest(url, 'delete', param, callback);

    };

    //异步请求
    function asyncRequest(url, type, param, callback) {

        $.ajax({
            url: url,
            cache: false,
            type: type,
            async: true,
            dataType: 'json',
            data: param,
            success: function (data, textStatus) {
                (callback && typeof (callback) === "function") && callback(data, textStatus);
            }
        });

    };

    //同步请求
    function syncRequest(url, type, param, callback) {

        $.ajax({
            url: url,
            cache: false,
            type: type,
            async: false,
            dataType: 'json',
            data: param,
            success: function (data, textStatus) {
                (callback && typeof (callback) === "function") && callback(data, textStatus);
            }
        });

    };

})(jQuery);