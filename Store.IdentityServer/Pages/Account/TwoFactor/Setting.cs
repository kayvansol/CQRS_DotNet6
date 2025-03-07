namespace Store.IdentityServer.Pages.Account.TwoFactor
{
    public class Setting
    {

        public string SmtpServer { get; set; }
        public string SenderAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int SmtpPort { get; set; }
    }
}
