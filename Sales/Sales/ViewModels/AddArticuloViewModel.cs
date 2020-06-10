namespace Sales.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Xamarin.Forms;

    public class AddArticuloViewModel : BaseViewModel
    {
        #region Atributos
        private bool isRunning;
        private bool isEnabled;
        #endregion 

        #region Propiedades
        public string  Descripcion { get; set; }

        public string Precio { get; set; }

        public string Comentario { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning , value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        #endregion 

        #region Constructores
        public AddArticuloViewModel()
        {
            this.isEnabled = true;
        }
        #endregion 

        #region Comandos
        public ICommand GuardarCommand
        {
            get
            {
                return new RelayCommand(Guardar);
            }
        }

        private async void Guardar()
        {
            if (string.IsNullOrEmpty(this.Descripcion))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorDescripcion , Languages.Aceptar);
                return;
            }
            if (string.IsNullOrEmpty(this.Precio))
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorPrecio , Languages.Aceptar);
                return;
            }

            var precio = decimal.Parse(this.Precio);
            if (precio <= 0)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.ErrorPrecio, Languages.Aceptar);
                return;
            }
        }

        public ICommand SeleccionarImagen
        {
            get
            {
                return new RelayCommand(SelImagen);
            }
        }

        private void SelImagen()
        {
            return;
        }
        #endregion
    }
}
