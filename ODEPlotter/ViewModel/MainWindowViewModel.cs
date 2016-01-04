using System.Collections.ObjectModel;
using ODEPlotter.Model;

namespace ODEPlotter.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _integrationType = OdeIntegrationType.Type.RungeKutta;
            _pointsNumber = "10";
        }

        private string _pointsNumber;

        public ObservableCollection<OdeIntegrationType> People { get; set; }

        public string PointsNumber
        {
            get { return _pointsNumber;}
            set
            {
                if (!_pointsNumber.Equals(value))
                {
                    _pointsNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private OdeIntegrationType.Type _integrationType;
        public OdeIntegrationType.Type IntegrationType
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
