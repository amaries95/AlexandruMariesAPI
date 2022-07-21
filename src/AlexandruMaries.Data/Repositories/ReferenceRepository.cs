using AlexandruMaries.Data;
using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Infrastructure.Interfaces;
using AlexandruMaries.Model;
using Microsoft.EntityFrameworkCore;

namespace AlexandruMaries.Infrastructure.Repositories
{
	public class ReferenceRepository : IReferenceRepository
	{
		private readonly AlexandruMariesDbContext _alexandruMariesDbContext;

		public ReferenceRepository(AlexandruMariesDbContext alexandruMariesDbContext)
		{
			_alexandruMariesDbContext = alexandruMariesDbContext;
		}

		public async Task<ReferenceResponse> AddNewReference(ReferenceRequest referenceRequest)
		{
			var reference = new Reference
			{
				Summary = referenceRequest.Summary,
				Author = referenceRequest.Author,
				JobTitleAuthor = referenceRequest.JobTitleAuthor
			};

			_alexandruMariesDbContext.References.Add(reference);
			await _alexandruMariesDbContext.SaveChangesAsync();

			return new ReferenceResponse
			{
				Id = reference.Id,
			};
		}

		public async Task<ReferenceResponse?> DeleteReference(int id)
		{
			var referenceToBeRemoved = await _alexandruMariesDbContext.References.FirstOrDefaultAsync(x => x.Id == id);

			if(referenceToBeRemoved != null)
			{
				_alexandruMariesDbContext.References.Remove(referenceToBeRemoved);
				await _alexandruMariesDbContext.SaveChangesAsync();

				return new ReferenceResponse { Id = id };
			}

			return null;
		}

		public IQueryable<Reference> GetAllReferences(bool returnVisibleReferencesOnly)
		{
			if(returnVisibleReferencesOnly)
			{
				return _alexandruMariesDbContext.References.Where(reference => reference.IsVisible == true);

			}

			return _alexandruMariesDbContext.References;


		}

		public async Task UpdateVisibilityOnReferences(ReferenceRequest referenceRequest)
		{
			foreach (var reference in _alexandruMariesDbContext.References)
			{
				if(referenceRequest.Id == reference.Id)
				{
					reference.IsVisible = referenceRequest.IsVisible;
				}
			}

			await _alexandruMariesDbContext.SaveChangesAsync();
		}
	}
}
