/************************************************************************
   HeBianGu.Control.Dock

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at https://opensource.org/licenses/MS-PL
 ************************************************************************/

using System;

namespace HeBianGu.Control.Dock.Themes
{
	/// <inheritdoc/>
	public class GenericTheme : Theme
	{
		/// <inheritdoc/>
		public override Uri GetResourceUri()
		{
			return new Uri("/HeBianGu.Control.Dock;component/Themes/Generic.xaml", UriKind.Relative);
		}
	}
}