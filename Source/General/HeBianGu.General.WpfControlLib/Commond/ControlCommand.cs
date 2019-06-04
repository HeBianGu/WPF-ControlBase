#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 四川*******有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[河边骨]   时间：2017/12/1 10:31:36 
 * 文件名：ControlCommand 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib.Commond
{
   public static class ControlCommand
    {

        /// <summary> 运行文件 </summary>
        public static ICommand OpenFileCommand
        {
            get
            {
                Action<object> action = l =>
                {
                    if(l is ListBox)
                    {
                        ListBox listbox = l as ListBox;

                        if (listbox.SelectedItem == null) return;

                        Process.Start(listbox.SelectedItem.ToString());
                    }
                   
                };

                return new RelayCommand(action);
            }
        }
    }
}
