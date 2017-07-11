using System;
using System.Net;

namespace WinChrome_HttpWatch
{
    public class TMWebClient : WebClient
    {
        private int _timeout;

        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public TMWebClient()
        {
            this._timeout = 30000;
        }

        public TMWebClient(int timeout)
        {
            this._timeout = timeout * 1000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }
    }
}
