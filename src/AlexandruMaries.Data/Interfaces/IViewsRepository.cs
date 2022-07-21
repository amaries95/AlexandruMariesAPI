using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexandruMaries.Data.Interfaces
{
	public interface IViewsRepository
	{
		Task<ViewsResponse> GetViews();

		Task UpdateViews();
	}
}
