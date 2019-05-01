namespace Shop.UIForms.Helpers
{
    using Interfaces;
    using Resources;
    using Xamarin.Forms;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept => Resource.Accept;
        public static string Error => Resource.Error;
        public static string EmailMessage => Resource.EmailError;
        public static string PasswordError => Resource.PasswordError;
        public static string DeleteProduct => Resource.DeleteProduct;
        public static string EmailError => Resource.EmailError;
        public static string EmailPasswordError => Resource.EmailPasswordError;
        public static string NameAboutPage => Resource.NameAboutPage;
        public static string NameLoginPage => Resource.NameLoginPage;
        public static string NameProductError => Resource.NameProductError;
        public static string NameSetupPage => Resource.NameSetupPage;
        public static string PriceError => Resource.PriceError;
        public static string PriceProductError => Resource.PriceProductError;
        public static string TitleAbout => Resource.TitleAbout;
        public static string TitleLogin => Resource.TitleLogin;
        public static string TitleSetup => Resource.TitleSetup;
        public static string Confirm => Resource.Confirm;
        public static string Yes => Resource.Yes;
        public static string No => Resource.No;
        public static string Login => Resource.Login;

        public static string EmailMEmailessage => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string Password => Resource.Password;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        //public static string PasswordMessage => Resource.PasswordMessage;

        public static string Remember => Resource.Remember;

        //public static string EmailOrPasswordIncorrect => Resource.EmailOrPasswordIncorrect;

    }


}
