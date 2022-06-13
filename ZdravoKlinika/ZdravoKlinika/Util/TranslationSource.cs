using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ZdravoKlinika.Util
{
    internal class TranslationSource : INotifyPropertyChanged
    {
        private static readonly TranslationSource instance = new TranslationSource();

        public static TranslationSource Instance
        {
            get { return instance; }
        }



        private readonly ResourceManager resourceManager = ZdravoKlinika.Resources.Localisation.Resources.ResourceManager;

        private CultureInfo currentCulture = new System.Globalization.CultureInfo("en");
        public CultureInfo CurrentCulture 
        {
            get { return this.currentCulture; }
            set 
            {
                if(this.currentCulture != value)
                {
                    this.currentCulture = value;
                    var @event = this.PropertyChanged;
                    if (@event != null)
                    {
                        @event.Invoke(this, new PropertyChangedEventArgs(string.Empty));
                    }
                }
            }
        }



        public string this[string key]
        {
            get
            {
                return this.resourceManager.GetString(key, this.CurrentCulture);
                
            }
        }

        public static void SetLanguage(string locale)
        {
            if (string.IsNullOrEmpty(locale)) locale = "en";
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo(locale);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class LocalizationExtension : Binding
    {
        public LocalizationExtension(string name) : base("[" + name + "]")
        {
            this.Mode = BindingMode.OneWay;
            this.Source = TranslationSource.Instance;
        }
    }
}
