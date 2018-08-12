using System;
using System.Threading.Tasks;

namespace CdnApiClientTest
{
    class Program
    {
        private const string UserNameKey = "-username";
        private const string PasswordKey = "-password";
        private const string ServiceIdKey = "-serviceid";

        static async Task Main(string[] args)
        {
            if (args.Length != 6)
            {
                throw new ArgumentException("Command line arguments number is incorrect");
            }

            var username = "";
            var password = "";
            var serviceId = "";
            ParseCommandLineArguments(ref username, ref password, ref serviceId, args);

            var clientTest = new CdnClientTest(username, password, serviceId);

            var getResult = await clientTest.GetEmptyTest();
            Console.WriteLine($"GET CDN services:");
            Console.WriteLine(getResult);

            var getResultWithQueryString = await clientTest.GetWithQueryStringTest();
            Console.WriteLine($"GET CDN service reports:");
            Console.WriteLine(getResultWithQueryString);

            var postResult = await clientTest.PostDataTest();
            Console.WriteLine($"Purge CDN service content:");
            Console.WriteLine(postResult);
        }

        private static void ParseCommandLineArguments(ref string username,
            ref string password,
            ref string serviceid,
            string[] arguments)
        {
            for (int i = 0; i < arguments.Length; i += 2)
            {
                switch(arguments[i])
                {
                    case UserNameKey:
                        username = arguments[i + 1];
                        break;
                    case PasswordKey:
                        password = arguments[i + 1];
                        break;
                    case ServiceIdKey:
                        serviceid = arguments[i + 1];
                        break;
                    default:
                        // ignore unknown keys
                        break;
                }
            }
        }
    }
}
