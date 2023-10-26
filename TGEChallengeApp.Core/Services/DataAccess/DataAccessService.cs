using Microsoft.Extensions.Logging;
using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TGEChallengeApp.Core.Interfaces;

namespace TGEChallengeApp.Core.Services.DataAccess
{
    public class DataAccessService : IDataAccessService
    {
        public DataAccessService()
        {
        }

        public async Task<IEnumerable<string>> Read(string fileLocation)
        {
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException();
            }
            return await File.ReadAllLinesAsync(fileLocation);
        }

        public async Task Write(string fileLocation, IEnumerable<string> strings)
        {
            try
            {
                await File.WriteAllLinesAsync(fileLocation, strings);
            }
            catch (DirectoryNotFoundException dirNotFoundException)
            {
                Console.WriteLine(dirNotFoundException.Message);
                // Create and try again
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Console.WriteLine(unauthorizedAccessException.Message);

                // Show a message to the user
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);

                // Show a message to the user
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                // Show general message to the user
            }
        }

        public async Task Append(string fileLocation, IEnumerable<string> strings)
        {
            try {
                await File.AppendAllLinesAsync(fileLocation, strings);
            }
            catch (DirectoryNotFoundException dirNotFoundException)
            {
                Console.WriteLine(dirNotFoundException.Message);
                // Create and try again
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Console.WriteLine(unauthorizedAccessException.Message);

                // Show a message to the user
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);

                // Show a message to the user
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                // Show general message to the user
            }
        }



    }
}
