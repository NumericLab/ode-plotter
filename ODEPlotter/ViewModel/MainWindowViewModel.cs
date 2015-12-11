using ODEPlotter.Model;

namespace ODEPlotter.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        IntegrationType.Type _integrationType;
        public IntegrationType.Type IntegrationType
        {
            get
            {
                return _integrationType;
            }
            set
            {
                if (_integrationType != value)
                {
                    _integrationType = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
