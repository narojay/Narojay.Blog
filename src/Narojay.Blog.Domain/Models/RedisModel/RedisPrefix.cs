﻿namespace Narojay.Blog.Domain.Models.RedisModel;

public class RedisPrefix
{
    public const string GetAllUser = "GetAllUser";
    public const string GetPost = "GetPost";
    public const string GetLeaveMessagePageAsync = "GetLeaveMessagePageAsync";
    public const string GetLeaveMessagePageCountAsync = "GetLeaveMessagePageCountAsync";
    public const string GetPostListAsync = "GetPostListAsync";
    public const string GetTagStatistics = "GetTagStatistics";
    public static string GetAdminNotice = "GetAdminNotice";
    public static string GetLabelSelect = "GetLabelSelect";

    public static string GetAboutMeContentAsync = "GetAboutMeContentAsync";
}