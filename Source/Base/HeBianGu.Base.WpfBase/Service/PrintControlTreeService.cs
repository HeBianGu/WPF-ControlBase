#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 四川*******有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[河边骨]   时间：2018/4/28 15:33:26 
 * 文件名：PrintControlTreeService 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 视觉树逻辑树 </summary>
    public class PrintControlTreeService
    {
        public static PrintControlTreeService Instance = new PrintControlTreeService();

        private PrintControlTreeService()
        {

        }

        /// <summary> 遍历视觉树 </summary>
        public IEnumerable<DependencyObject> PrintVisualTree(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                PrintVisualTree(VisualTreeHelper.GetChild(obj, i));
            }

            yield return obj;
        }


        /// <summary> 遍历逻辑树 </summary>
        public IEnumerable<DependencyObject> PrintLogicalTree(DependencyObject obj)
        {
            foreach (var v in LogicalTreeHelper.GetChildren(obj))
            {
                if (v is DependencyObject)
                {
                    PrintLogicalTree(v as DependencyObject);
                }
            }

            yield return obj;
        }


        /// <summary> 遍历子匹配子类型 </summary>
        public IEnumerable<T> FindAllVisualChild<T>(DependencyObject obj, Predicate<T> match) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                {
                    if (match(child as T))
                    {
                        yield return (T)child;
                    }
                }
                else
                {
                    FindAllVisualChild<T>(child, match);
                }
            }
        }
    }
}
