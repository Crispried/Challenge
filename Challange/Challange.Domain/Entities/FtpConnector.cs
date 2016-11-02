using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class FtpConnector
    {
        private string hostName;

        private string login;

        private string password;

        public FtpConnector(string hostName, string login, string password)
        {
            this.hostName = hostName;
            this.login = login;
            this.password = password;
        }

        public bool IsFtpConnectionSuccessful()
        {
            if (!string.IsNullOrEmpty(hostName))
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(hostName);
                request.Proxy = null;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(login, password);
                try
                {
                    request.GetResponse();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    request.Abort();
                }
            }
            return false;
        }
    }
}
