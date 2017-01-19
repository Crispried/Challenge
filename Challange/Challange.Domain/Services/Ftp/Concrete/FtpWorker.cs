using Challange.Domain.Services.Ftp.Abstract;
using System.Net;

namespace Challange.Domain.Services.Ftp.Concrete
{
    public class FtpWorker : IFtpWorker
    {
        private string hostName;

        private string login;

        private string password;

        public FtpWorker(string hostName, string login, string password)
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
