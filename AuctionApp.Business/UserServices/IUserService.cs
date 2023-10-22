using AuctionApp.Domain.Enteties;
using System.Threading.Tasks;

namespace AuctionApp.Business.UserServices
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);

    }
}