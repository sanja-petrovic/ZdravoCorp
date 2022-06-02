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

namespace ZdravoKlinika.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryCreateMeeting.xaml
    /// </summary>
    public partial class SecretaryCreateMeeting : Page
    {
        
        public SecretaryCreateMeeting()
        {
            DataContext = new SecretaryViewModel.MeetingViewModel();
            InitializeComponent();
        }
    }
}