using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avonale_ApiGit.Models
{
	public class Repositorio
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Owner Owner { get; set; }
		public string Language { get; set; }
		public string Updated_at { get; set; }

		public List<string> Contributors;

		public Repositorio()
		{
			Contributors = new List<string>();
		}
	}
}
