using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Data.RepoModels.NewFolder;

namespace AlexandruMaries.Data.Interfaces
{
	public interface IUserRepository
	{
		public Task<UserRespone> Register(string user, string password);

		public Task<ServiceResponse<string>> Login(string username, string password);
	}
}
