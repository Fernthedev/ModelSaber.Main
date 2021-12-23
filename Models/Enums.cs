using System;
using System.ComponentModel;

namespace ModelSaber.Database.Models
{
    [Description("Platform type")]
    public enum Platform : byte
    {
        [Description("Platform PC")]
        Pc,
        [Description("Platform Quest")]
        Quest
    }

    [Flags, Description("Model status")]
    public enum Status : byte
    {
        [Description("Unpublished")]
        Unpublished = 0,
        [Description("Published")]
        Published = 1,
        [Description("ApprovalRequired")]
        ApprovalRequired = 2,
        [Description("Approved")]
        Approved = 4,
        [Description("Featured")]
        Featured = 8
    }

    public enum ThumbnailEnum : byte
    {
        Image,
        Video
    }

    public enum TypeEnum : byte
    {
        Avatar,
        Note,
        Platform,
        Saber,
        Effect,
        Wall,
        HealthBar
    }

    public static class EnumExtensions {
        public static string GetExt(this ThumbnailEnum en)
        {
            return en switch
            {
                ThumbnailEnum.Image => ".webp",
                ThumbnailEnum.Video => ".webm",
                _ => ".webp"
            };
        }
    }
}