using FSPBook.Portal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSPBook.Portal.Services
{
    public interface INewsService
    {
        public Task<List<Datum>> GetNews();
    }
}
