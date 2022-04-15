using System;
using Xamarin.Forms;

namespace XFDataTemplateSelectorSample
{
    public class SubscribeDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SubscribeTemplate { get; set; }
        public DataTemplate OtherTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item.ToString().Contains("subscribe"))
                return SubscribeTemplate;

            return OtherTemplate;
        }
    }
}
