using System;

namespace ModelSaber.Database.Models
{
    public enum Platform : byte
    {
        Pc,
        Qurst
    }

    [Flags]
    public enum Status : byte
    {
        Unpublished = 0,
        Published = 1,
        ApprovalRequired = 2,
        Approved = 4,
        Featured = 8
    }

    public enum ThumbnailEnum : byte
    {
        None,
        ImageGif,
        ImageJpg,
        ImagePng,
        ImageWebm
    }

    public enum TypeEnum : byte
    {
        Avatar,
        Note,
        Platform,
        Saber
    }
}