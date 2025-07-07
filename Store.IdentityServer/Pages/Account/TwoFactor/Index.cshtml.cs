
using Microsoft.AspNetCore.Cors;

namespace Store.IdentityServer.Pages.Account.TwoFactor
{
    [SecurityHeaders]
    [AllowAnonymous]
    [EnableCors("MyPolicy")]
    public class Index : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IIdentityProviderStore _identityProviderStore;

        public ViewModel View { get; set; }

        public Index(

            IIdentityServerInteractionService interaction,
            IAuthenticationSchemeProvider schemeProvider,
            IIdentityProviderStore identityProviderStore,
            IEventService events,
            SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _identityProviderStore = identityProviderStore;
            _events = events;
        }

        public void OnGet(InputModel param)
        {
            Input = param;
            ModelState.Clear();
        }

        public async Task<IActionResult> OnPost()
        {
            if (Input != null)
            {
                if (Input.TwoFactor != "746894")
                {
                    ModelState.AddModelError(string.Empty, LoginOptions.InvalidTwoFactorErrorMessage);

                    return Page();
                }
            }

            var context = await _interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

            // find user by username
            var user = await _signInManager.UserManager.FindByNameAsync(Input.Username);

            await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

            // only set explicit expiration here if user chooses "remember me". 
            // otherwise we rely upon expiration configured in cookie middleware.
            AuthenticationProperties props = null;

            if (LoginOptions.AllowRememberLogin && Input.RememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(LoginOptions.RememberMeLoginDuration)
                };
            };

            // issue authentication cookie with subject ID and username
            var isuser = new IdentityServerUser(user.Id)
            {
                DisplayName = user.UserName
            };

            await HttpContext.SignInAsync(isuser, props);

            if (context != null)
            {
                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                return Redirect(Input.ReturnUrl);
            }

            // request for a local page
            if (Url.IsLocalUrl(Input.ReturnUrl))
            {
                return Redirect(Input.ReturnUrl);
            }
            else if (string.IsNullOrEmpty(Input.ReturnUrl))
            {
                return Redirect("~/");
            }
            else
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

        }

    }


}