using HeBianGu.Base.WpfBase;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class PathPropery : PropertyInfo
    {
        public PathPropery()
        {
            SearchCommand = new RelayCommand(l =>
            {
                OpenFileDialog open = new OpenFileDialog();

                var result = open.ShowDialog();

                if (!result.HasValue) return;

                if (!result.Value) return;

                Text = open.FileName;
            });
        }

        public RelayCommand SearchCommand { get; set; }
    }
}
