using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTLII.Services
{
    interface IBooksRepository
    {
        IEnumerable<Type> GetBooks();
    }
}
