using AlexandruMaries.Data.Interfaces;
using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexandruMaries.Data.Repositories
{
	public class ViewsRepository : IViewsRepository
	{
		private readonly AlexandruMariesDbContext alexandruMariesDbContext;

		public ViewsRepository(AlexandruMariesDbContext alexandruMariesDbContext)
		{
			this.alexandruMariesDbContext = alexandruMariesDbContext;
		}

		public async Task<ViewsResponse> GetViews()
		{
			var result = await alexandruMariesDbContext.Views.FirstAsync();

			return new ViewsResponse { NumberOfViews = result.NumberOfViews };
		}

		public async Task UpdateViews()
		{
			if(!alexandruMariesDbContext.Views.Any())
			{
				var view = new View 
				{
					NumberOfViews = 0
				};

				alexandruMariesDbContext.Views.Add(view);
				await alexandruMariesDbContext.SaveChangesAsync();
			}

			alexandruMariesDbContext.Views.First().NumberOfViews++;
			await alexandruMariesDbContext.SaveChangesAsync();
		}
	}
}
