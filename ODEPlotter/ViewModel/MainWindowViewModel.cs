using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ODEPlotter.Model;

namespace ODEPlotter.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            _integrationType = OdeIntegrationType.Types.First();
            _pointsNumber = "10";
            IntegrationTypes = OdeIntegrationType.Types;
            CalculateCommand = new RelayCommand(Calculate);
        }

        public RelayCommand CalculateCommand { get; set; }

        public List<string> IntegrationTypes { set; get; }

        private void Calculate(object param)
        {
            MessageBox.Show("Executed");
        }

        private string _pointsNumber;
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

        private string _integrationType;
        public string IntegrationType
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
