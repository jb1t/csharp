using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace PaperAffiliations
{
    public interface IPopulateRecord<T>
    {
        T PopulateRecord(string line);
    }
}