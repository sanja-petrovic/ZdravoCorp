﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoKlinika.View
{
    /// <summary>
    /// Interaction logic for PatientAddView.xaml
    /// </summary>
    public partial class PatientAddView : Page
    {
        public PatientAddView()
        {
            InitializeComponent();
            priorityComboBox.Items.Add("Vreme");
            priorityComboBox.Items.Add("Doktor");
            priorityComboBox.SelectedIndex = 0;
        }
    }
}
