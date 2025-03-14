namespace ecommerce.auth_ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase
    {
		AuthService _authservice;
		public AuthController (AuthService authservice) 
		{
			_authservice = authservice;
		}
        [HttpPost ("Register")]
		public string Register ([FromBody] string x)
		{
			return _authservice.Stringa(x);
		}
    }
}

