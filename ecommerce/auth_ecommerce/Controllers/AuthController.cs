namespace ecommerce.auth_ecommerce.Controllers
{
    [ApiController]
    [Route("api/auth/[controller]")]
    public class AuthController: ControllerBase
    {
		AuthService _authservice;
		public AuthController (AuthService authservice) 
		{
			_authservice = authservice;
		}
		[HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UtentiDto utenti)
        {
            var response = await _auth.Register(
                new Utenti {Username = utenti.Username}, utenti.Password
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UtentiDto utenti)
        {
            var response = await  _auth.Login(utenti.Username, utenti.Password);
            if(!response.Success)
            {
                BadRequest(response.Message = "Login non effettuato");
            }
            return Ok(response);
        }
    }
}

