﻿using PowerSwitcher.TrayApp.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PowerSwitcher.TrayApp
{
    public class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<IPowerSchema> Schemas;

        private IPowerManager pwrManager;
        public MainWindowViewModel(IPowerManager powerManager)
        {
            this.pwrManager = powerManager;
            pwrManager.PropertyChanged += PwrManager_PropertyChanged;

            Schemas = new ObservableCollection<IPowerSchema>(pwrManager.PowerSchemas);
        }

        private void PwrManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //TODO: Do better binding do underlying model
            if(e.PropertyName == nameof(IPowerManager.PowerSchemas))
            {
                Schemas.Clear();
                pwrManager.PowerSchemas.ForEach(sch => Schemas.Add(sch));
            }
            else
            {
                throw new InvalidOperationException("Invalid property changed on IPowerManager");
            }
        }
    }
}
