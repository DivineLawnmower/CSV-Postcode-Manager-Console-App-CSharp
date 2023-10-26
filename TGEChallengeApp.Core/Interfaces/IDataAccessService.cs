using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGEChallengeApp.Core.Interfaces
{
    public interface IDataAccessService
    {
        public Task<IEnumerable<string>> Read(string fileLocation);

        public Task Write(string fileLocation, IEnumerable<string> strings);


        public Task Append(string fileLocation, IEnumerable<string> strings);

    }
}
