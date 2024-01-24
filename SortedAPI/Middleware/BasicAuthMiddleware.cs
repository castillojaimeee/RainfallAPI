using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace SortedAPI.Middleware
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        IConfiguration configuration;
        public BasicAuthMiddleware(RequestDelegate next)
        {
            this._next = next;
            IConfigurationBuilder _builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false);
            IConfigurationRoot _configuration = _builder.Build();
            configuration = _configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string authToken = string.Empty;
                if (IsValidAuthorization(context, out authToken))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(authToken), null);
                    await this._next.Invoke(context);
                }
                else
                {
                    UnauthorizedHandling(context);
                }
            }
            catch
            {
                UnauthorizedHandling(context);
            }
        }

        internal void UnauthorizedHandling(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        private bool IsValidAuthorization(HttpContext context, out string authToken)
        {
            bool isValid = false;
            authToken = string.Empty;
            try
            {
                string authHeader = context.Request.Headers["Authorization"];

                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    authHeader = authHeader.Replace("Basic ", "");
                    authToken = authHeader;
                    var decodeAuthHeader = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authHeader));

                    var userCredentials = decodeAuthHeader.Split(':');

                    if (userCredentials.Length == 2)
                    {
                        string userName;
                        string passWord;
                        userName = userCredentials[0];
                        passWord = CreateMD5(userCredentials[1]);

                        if ((userName == configuration["Username"]) &&(passWord == configuration["Password"].ToUpper()))
                        {
                            isValid = true;
                        }
                    }
                }
                return isValid;
            }
            catch
            {
                throw;
            }
        }

        public static string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
