using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexandruMaries.Data.RepoModels
{
	public class ServiceResponse<T>
	{
		public int Id { get; set; }

		public bool Success { get; set; }

		public string Message { get; set; }

		public T Data { get; set; }
	}
}
