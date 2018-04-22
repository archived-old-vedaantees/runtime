namespace Vedaantees.Hosts.SingleSignOn.Presentation.Models
{
    public class LoginViewModel : LoginInputViewModel
    {
        public LoginViewModel()
        {
            
        }

        public LoginViewModel(LoginInputViewModel viewModel)
        {
            Username = viewModel.Username;
            Password = viewModel.Password;

        }
    }
}
