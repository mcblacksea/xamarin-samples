﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace XF.Material.Forms.UI.Internals
{
    internal class MenuDialogLabel : MaterialLabel
    {
        public static readonly BindableProperty SizeChangeCommandProperty = BindableProperty.Create(nameof(SizeChangeCommand), typeof(ICommand), typeof(MenuDialogLabel));

        public ICommand SizeChangeCommand
        {
            get => (ICommand)GetValue(SizeChangeCommandProperty);
            set => SetValue(SizeChangeCommandProperty, value);
        }

        public MenuDialogLabel() => SizeChanged += MenuDialogLabel_SizeChanged;

        private void MenuDialogLabel_SizeChanged(object sender, EventArgs e)
        {
            SizeChangeCommand?.Execute(new Dictionary<string, object>
            {
                {"width", Width },
                {"parameter", Convert.ToInt32(GetValue(MaterialMenuDialog.ParameterProperty)) }
            });

            SizeChanged -= MenuDialogLabel_SizeChanged;
        }
    }
}
