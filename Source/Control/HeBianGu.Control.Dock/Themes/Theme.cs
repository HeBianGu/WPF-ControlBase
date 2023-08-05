/************************************************************************
   HeBianGu.Control.Dock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
 ************************************************************************/

using System;
using System.Windows;

namespace HeBianGu.Control.Dock.Themes
{
	/// <summary>Provides a base class for the implementation of a custom HeBianGu.Control.Dock WPF theme.</summary>
	public abstract class Theme : DependencyObject
	{
		/// <summary>Class constructor</summary>
		public Theme()
		{
		}

		/// <summary>Gets the <see cref="Uri"/> of the XAML that contains the definition for this HeBianGu.Control.Dock theme.</summary>
		/// <returns><see cref="Uri"/> of the XAML that contains the definition for this custom HeBianGu.Control.Dock theme</returns>
		public abstract Uri GetResourceUri();
	}
}
