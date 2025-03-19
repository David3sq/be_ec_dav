namespace ecommerce.auth_ecommerce.Services
{
    public class AuthService
    {
		private readonly EconContext _context;
		private readonly Log _log;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;
		public AuthService(EcomContext ecomcontext, Log log, IConfiguration configuration)
		{
			_context = ecomcontext;
			_log = log;
			_configuration = configuration;
		}

		public async Task<ServiceResponse<int>> Register (Utenti utenti)
		{
			var response = new ServiceResponse<int>();
			if(await UserExists(untenti.Username))
			{
				response.Message = "Utente già registrato";
				response.Success = false;
				return response;
			}
			
			CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            utenti.PasswordHash = passwordHash;
            utenti.PasswordSalt = passwordSalt;

			_context.untenti.Add(utenti);
			await _context.SaveChangeAsync();
			//imposta la variabile globale che indica l'identificativo dell utente ID
            _log.Savelog = utenti.ID;
            
            response.Data = utenti.ID;
            response.Success = true;
            response.Message = "Utente registrato con successo.";
            
            return response;
		}

		public async Task<ServiceResponse<int>> Login(Utenti utenti)
		{
				
		}
    }
}

