using System.Threading.Tasks;

namespace MUG_App.RestService
{
    public interface IRestService
    {
        Task<dynamic> GetData(string restUrl);
    }
}