using System.Threading.Tasks;

namespace LogParser.Infrastructure.Http.Client
{
    public interface IInnerHttpClient
    {
        Task<string> Execute(string request);
        Task<TOut> Execute<TOut>(string request);
    }
}