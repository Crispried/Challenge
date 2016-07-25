using Challange.Domain.Infrastructure;
using Challange.Domain.SettingsService.SettingTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Challange.Domain.SettingsService.SettingParser
{
    public class PlayerPanelSettingsParser : ISettingsParser<PlayerPanelSettings>
    {
        public bool SaveSettings(PlayerPanelSettings settings,
                                string settingsFilePath)
        {
            if (FileWorker.SerializeXml(settings, settingsFilePath))
            {
                return true;
            }
            return false;
        }

        public PlayerPanelSettings GetSettings(string settingsFilePath)
        {
            XmlSerializer serializer = new
                    XmlSerializer(typeof(PlayerPanelSettings));
            FileStream fs = new FileStream(settingsFilePath,
                                            FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            PlayerPanelSettings playerPanelSettings;
            playerPanelSettings = (PlayerPanelSettings)serializer.
                                            Deserialize(reader);
            fs.Close();
            return playerPanelSettings;
        }
    }
}
