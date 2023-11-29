using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Model;

namespace AlexandruMaries.Data.Interfaces
{
	public interface IReferenceRepository
	{
		public Task<ReferenceResponse> AddNewReference(ReferenceRequest reference);
		public Task<ReferenceResponse?> DeleteReference(int id);
		public Task<IEnumerable<Reference>> GetAllReferences();

        public Task<IEnumerable<Reference>> GetAllVisibleReferences();

		public Task UpdateVisibilityOnReferences(ReferenceRequest references);
    }
}
