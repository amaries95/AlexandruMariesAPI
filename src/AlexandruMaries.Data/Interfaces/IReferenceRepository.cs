using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Model;

namespace AlexandruMaries.Infrastructure.Interfaces
{
	public interface IReferenceRepository
	{
		public Task<ReferenceResponse> AddNewReference(ReferenceRequest reference);
		public Task<ReferenceResponse?> DeleteReference(int id);
		public IQueryable<Reference> GetAllReferences(bool returnVisibleReferencesOnly);
		public Task UpdateVisibilityOnReferences(ReferenceRequest references);
	}
}
