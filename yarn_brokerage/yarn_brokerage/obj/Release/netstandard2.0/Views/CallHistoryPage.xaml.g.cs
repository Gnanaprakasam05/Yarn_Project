//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("yarn_brokerage.Views.CallHistoryPage.xaml", "Views/CallHistoryPage.xaml", typeof(global::yarn_brokerage.Views.CallHistoryPage))]

namespace yarn_brokerage.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\CallHistoryPage.xaml")]
    public partial class CallHistoryPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry txtSearch;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Image imgPreviousDate;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.DatePicker startDatePicker;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Image imgNextDate;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Image butFilter;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblFilter;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Image butClear;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Label lblClear;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView CallHistoryListView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(CallHistoryPage));
            txtSearch = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "txtSearch");
            imgPreviousDate = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Image>(this, "imgPreviousDate");
            startDatePicker = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.DatePicker>(this, "startDatePicker");
            imgNextDate = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Image>(this, "imgNextDate");
            butFilter = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Image>(this, "butFilter");
            lblFilter = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblFilter");
            butClear = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Image>(this, "butClear");
            lblClear = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Label>(this, "lblClear");
            CallHistoryListView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "CallHistoryListView");
        }
    }
}
