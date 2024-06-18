using Assignment13750.Models;

namespace Assignment13750.Data
{
	public interface ICredentialManager
	{
		public void Add(Credentials credentials);
		public void Update(Credentials credentials);
		public void Delete(int id);
		public Credentials GetCredById(int id);
		public List<Credentials> GetAllCredentials();
		public void DropClassTuition(Credentials credentials);
		public void AddClassTuition(Credentials credentials);
	}
}
