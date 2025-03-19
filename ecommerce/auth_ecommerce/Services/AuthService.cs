namespace ecommerce.auth_ecommerce.Services
{
    public class AuthService
    {
		private readonly EcomContext _context;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		public AuthService(EcomContext ecomcontext, IConfiguration configuration)
		{
			_context = ecomcontext;
			_configuration = configuration;
		}

		public async Task<ServiceResponse<int>> Register (Utenti utenti, string password)
		{
			var response = new ServiceResponse<int>();
			if(await UserExists(utenti.Username))
			{
				response.Message = "Utente già registrato";
				response.Success = false;
				return response;
			}
			
			CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            utenti.PasswordHash = passwordHash;
            utenti.PasswordSalt = passwordSalt;

			_context.Utenti.Add(utenti);
			await _context.SaveChangesAsync();
			//imposta la variabile globale che indica l'identificativo dell utente ID
            
            response.Data = utenti.Id;
            response.Success = true;
            response.Message = "Utente registrato con successo.";
            
            return response;
		}

		public async Task<ServiceResponse<int>> Login(Utenti utenti)
		{
			var response = new ServiceResponse<int>();
			return response;
		}
    }
}

