using System.Data;
using AlexandruMaries.Data.Constants;
using AlexandruMaries.Data.Interfaces;
using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Model;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace AlexandruMaries.Data.Repositories
{
	public class ReferenceRepository : IReferenceRepository
	{
		private readonly AlexandruMariesDbContext _alexandruMariesDbContext;
		private readonly IDapperContext _dapperContext;

        public ReferenceRepository(AlexandruMariesDbContext alexandruMariesDbContext, IDapperContext dapperContext)
        {
            _alexandruMariesDbContext = alexandruMariesDbContext;
            _dapperContext = dapperContext;
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

		public async Task<IEnumerable<Reference>> GetAllReferences()
		{
			return await _alexandruMariesDbContext.References
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Reference>> GetAllVisibleReferences()
        {
            IEnumerable<Reference> response;

            using (IDbConnection dbConnection = _dapperContext.GetConnection())
            {
                response = await dbConnection.QueryAsync<Reference>(QueryConstants.GetAllVisibleReferences);
            }

			return response.ToList();
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
