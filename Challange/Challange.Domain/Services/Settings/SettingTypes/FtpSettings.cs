﻿
namespace Challange.Domain.Services.Settings.SettingTypes
{
    public class FtpSettings : Setting
    {
        public string FtpAddress { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
