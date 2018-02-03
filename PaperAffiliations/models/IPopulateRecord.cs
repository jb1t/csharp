using System.Collections.Generic;

namespace PaperAffiliations
{
    public interface IPopulateRecord<T>
    {
        T PopulateRecord(string line);
    }
}