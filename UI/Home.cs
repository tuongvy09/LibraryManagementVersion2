﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementVersion2
{
    public partial class Home : Form
    {
        private string currentRole;
        public Home(string role)
        {
            InitializeComponent();
            currentRole = role;
            InitializeMenuButtons(currentRole);

        }
    }
}
